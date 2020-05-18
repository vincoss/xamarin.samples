using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;


namespace Xamarin_SqliteCipher.Test.Services
{
    public abstract class BaseSqliteDatabaseEngine : IDisposable
    {
        private bool _disposed;
        private Lazy<SQLiteAsyncConnection> lazyInitializer;

        protected BaseSqliteDatabaseEngine()
        {
            lazyInitializer = new Lazy<SQLiteAsyncConnection>(() => { return Create(); });
        }

        #region Public methods

        public Task CloseConnectionAsync()
        {
            return Database.CloseAsync();
        }

        public async Task DeleteDatabaseAsync()
        {
            var path = GetDatabasePath();
            if (File.Exists(path))
            {
                await CloseConnectionAsync();
                FileDelete(path);
            }
        }

        public abstract string GetDatabaseKey();

        public abstract string GetDatabasePath();

        public abstract SQLiteOpenFlags GetFlags();

        public abstract Task CreateDatabaseAsync();

        #endregion

        #region Private nethods

        private SQLiteAsyncConnection Create()
        {
            var path = GetDatabasePath();
            var key = GetDatabaseKey();
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
            if(_disposed)
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

        public SQLiteAsyncConnection Database
        {
            get { return lazyInitializer.Value; }
        }
    }
}