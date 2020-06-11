using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;


namespace Xamarin_SqliteCipher.Data
{
    public class SqliteDatabaseRestoreResult
    {
        public SqliteDatabaseRestoreResult(Exception exception, bool correctDatabase = false, bool migrationRequired = false)
        {
            Exception = exception;
            IsCorrectDatabase = correctDatabase;
            IsMigrationRequired = migrationRequired;
            IsSuccessful = Exception == null && IsCorrectDatabase;
        }

        public bool IsSuccessful { get; private set; }
        public bool IsCorrectDatabase { get; private set; }
        public bool IsMigrationRequired { get; private set; }
        public Exception Exception { get; private set; }
    }

    public class SqliteDatabaseConfiguration
    {
        public string DatabasePath { get; set; } = Path.Combine(AppContext.BaseDirectory, "Database.db3");
        public string DatabaseKey { get; set; } = "Pass@word1";
    }

    public interface ISqliteDatabaseContext
    {
        bool DatabaseExists();
        Task ChangeDbKeyAsync(string key, string rekey);
        Task CreateDatabaseAsync();
        Task CloseConnectionAsync();
        Task DeleteDatabaseAsync();
        Task<SqliteDatabaseRestoreResult> RestoreAsync(string restorePath);
    }

    /// <summary>
    /// https://www.sqlite.org/docs.html
    /// </summary>
    public abstract class BaseSqliteDatabaseEngine : ISqliteDatabaseContext, IDisposable
    {
        private bool _disposed;
        private static readonly object _loker = new object();
        private readonly SqliteDatabaseConfiguration _configuration;

        public BaseSqliteDatabaseEngine(SqliteDatabaseConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            _configuration = configuration;
            Validate(_configuration.DatabasePath, _configuration.DatabaseKey);
        }

        #region Public methods

        public bool DatabaseExists()
        {
            return File.Exists(_configuration.DatabasePath);
        }

        public async Task<bool> TableExistsAsync(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException(nameof(tableName));
            }
            var result = await Database.ExecuteScalarAsync<int>($"SELECT COUNT(type) FROM sqlite_master WHERE type = 'table' AND name = '{tableName}';");
            return result > 0;
        }

        public async Task CloseConnectionAsync()
        {
            if (_database == null)
            {
                return;
            }
            await _database.CloseAsync();
            _database = null;
        }

        public async Task DeleteDatabaseAsync()
        {
            if (DatabaseExists())
            {
                await CloseConnectionAsync();
                FileDelete(_configuration.DatabasePath);
            }
        }

        public async Task ChangeDbKeyAsync(string key, string rekey)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (string.IsNullOrWhiteSpace(rekey))
            {
                throw new ArgumentNullException(nameof(rekey));
            }
            await Database.ExecuteAsync($"PRAGMA key = '{key}';");
            await Database.ExecuteAsync($"PRAGMA rekey = '{rekey}';");
        }

        public abstract Task CreateDatabaseAsync();

        public async Task<SqliteDatabaseRestoreResult> RestoreAsync(string restorePath)
        {
            if (string.IsNullOrWhiteSpace(restorePath))
            {
                throw new ArgumentNullException(nameof(restorePath));
            }

            var path = Path.GetDirectoryName(_configuration.DatabasePath);
            var tempPath = Path.Combine(path, $"{DateTime.Now:yyyyMMddHHmmssfffff}-RestoreDb.db3");
            SQLiteAsyncConnection connection = null;

            try
            {
                File.Copy(restorePath, tempPath);

                // Current database close connection.
                await CloseConnectionAsync();

                // Check wheter can connect, might not be Sqlite database or wrong key that might throw.
                var options = new SqliteDatabaseConfiguration
                {
                    DatabasePath = tempPath,
                    DatabaseKey = _configuration.DatabaseKey
                };

                // Can connect? It might throw.
                connection = CreateConnection(options);
                var result = await connection.ExecuteScalarAsync<int>($"SELECT COUNT(type) FROM sqlite_master;");

                // Is correct?
                var validDatabase = await IsCorrectDatabase(connection);

                // Need upgrade?
                var migrationRequired = await IsUpgradeRequired(connection);
                await connection.CloseAsync();
                connection = null;

                // Drop current database and move restored. Since got here that restore database shall be OK.
                await DeleteDatabaseAsync();
                File.Move(tempPath, _configuration.DatabasePath);

                return new SqliteDatabaseRestoreResult(null, validDatabase, migrationRequired);
            }
            catch (Exception ex)
            {
                return new SqliteDatabaseRestoreResult(ex);
            }
            finally
            {
                if(connection != null)
                {
                    await connection.CloseAsync();
                    connection = null;
                }
                File.Delete(tempPath);
            }
        }

        #endregion

        #region Private nethods

        protected abstract SQLiteOpenFlags GetFlags();
        protected abstract Task<bool> IsCorrectDatabase(SQLiteAsyncConnection connection);
        protected abstract Task<bool> IsUpgradeRequired(SQLiteAsyncConnection connection);

        protected SQLiteAsyncConnection CreateConnection(SqliteDatabaseConfiguration configuration)
        {
            var path = configuration.DatabasePath;
            var key = configuration.DatabaseKey;
            var flags = GetFlags();
            Validate(path, key);
            var options = new SQLiteConnectionString(path, flags, true, key: key);
            return new SQLiteAsyncConnection(options);
        }

        private void Validate(string path, string key)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
        }

        private static bool FileDelete(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            bool flag;
            var attempts = 0;
            Start:

            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                flag = true;
            }
            catch
            {
                if (attempts >= 10)
                {
                    throw;
                }
                System.Threading.Thread.Sleep(100);
                attempts++;
                goto Start;
            }

            return flag;
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected async virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if (Database != null)
                {
                    await CloseConnectionAsync();
                }
            }

            _disposed = true;
        }

        #endregion

        private SQLiteAsyncConnection _database;

        public SQLiteAsyncConnection Database
        {
            get
            {
                if (_database == null)
                {
                    lock (_loker)
                    {
                        if (_database == null)
                        {
                            _database = CreateConnection(_configuration);
                        }
                    }
                }
                return _database;
            }
        }
    }
}
