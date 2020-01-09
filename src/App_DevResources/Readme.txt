
## Tasks to learn


MenuItem from class instead of xaml
viss git logo
font awsome icons samples
image zoon and pan, see gestures
Master view
sync (find all resources)
GPS UTM
	https://en.wikipedia.org/wiki/Geocode

rhino licencing find source samples and update
canon uwp

localization
	see xamarin-fomms-samples TodoLocalized
	see xamarin-fomms-samples TodoLocalizedRTL
	see xamarin-fomms-samples UsingResxLocalization
	see xamarin-fomms-samples UsingResxLocalization
	C:\_Data\GitHub\Xamarin\docs-archive\Samples\TodoLocalized
	C:\_Data\GitHub\Xamarin\docs-archive\Samples\UsingResxLocalization
	https://www.romaniancoder.com/set-culture-for-all-threads-in-c-apps/
	https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/localization/

telemetry
	See https://docs.microsoft.com/en-us/appcenter/
	dbsite, navigation, last used,location,errors and many more

## Next
ListView
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/
https://github.com/dotnet-architecture/News
https://dotnet.microsoft.com/learn/dotnet/architecture-guides

review
https://github.com/Daddoon/BlazorMobile
https://github.com/bramborman/UWPHelper 

## Temp

server API
C:\_Data\GitHub\aspnet\AspNetCore.Docs\aspnetcore\mobile
https://docs.microsoft.com/en-us/aspnet/core/mobile/native-mobile-backend?view=aspnetcore-3.1
https://docs.microsoft.com/en-us/archive/msdn-magazine/2016/august/asp-net-core-real-world-asp-net-core-mvc-filters
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/authentication-and-authorization

compiler warning to errors settings

all database calls catch (Exception ex)
{
 Debug.WriteLine("Error: ", ex.Message); // TODO: replace with log
}

// file path
 string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.txt");
 
 // database
 var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
 
 // Rest service
 https://docs.microsoft.com/en-us/xamarin/xamarin-forms/data-cloud/web-services/rest

 public class TodoItemDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public TodoItemDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(TodoItem).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(TodoItem)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<TodoItem>> GetItemsAsync()
        {
            return Database.Table<TodoItem>().ToListAsync();
        }

        public Task<List<TodoItem>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<TodoItem> GetItemAsync(int id)
        {
            return Database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(TodoItem item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(TodoItem item)
        {
            return Database.DeleteAsync(item);
        }
    }


## Sync



 public enum ConflictResolution
    {
        /// <summary>
        /// Indicates that the change on the server is the conflict winner
        /// </summary>
        ServerWins,

        /// <summary>
        /// Indicates that the change sent by the client is the conflict winner
        /// </summary>
        ClientWins,

        /// <summary>
        /// Indicates that you will manage the conflict by filling the final row and sent it to both client and server
        /// </summary>
        MergeRow
    }
	
/// <summary>
    /// Sync direction : Can be Bidirectional (default), DownloadOnly, UploadOnly
    /// </summary>
    public enum SyncDirection
    {
        /// <summary>
        /// Table will be sync from server to client and from client to server
        /// </summary>
        Bidirectional = 1,

        /// <summary>
        /// Table will be sync from server to client only.
        /// All changes occured client won't be uploaded to server
        /// </summary>
        DownloadOnly = 2,

        /// <summary>
        /// Table will be sync from client to server only
        /// All changes from server won't be downloaded to client
        /// </summary>
        UploadOnly = 3
    }

    public enum SyncWay
    {
        /// <summary>
        /// No sync engaged
        /// </summary>
        None = 0,

        /// <summary>
        /// Sync is selecting then downloading changes from server
        /// </summary>
        Download = 1,

        /// <summary>
        /// Sync is selecting then uploading changes from client
        /// </summary>
        Upload = 2
    }
	
	/// <summary>
    /// Sync progress step. Used for the user feedback
    /// </summary>
    public enum SyncStage
    {
        None,
        BeginSession,
        ScopeLoading,
        ScopeSaved,

        SchemaReading,
        SchemaRead,
        SchemaApplying,
        SchemaApplied,
        TableSchemaApplying,
        TableSchemaApplied,

        TableChangesSelecting,
        TableChangesSelected,

        DatabaseChangesApplying,
        DatabaseChangesApplied,
        TableChangesApplying,
        TableChangesApplied,

        EndSession,
        CleanupMetadata
    }
	
	/// <summary>
    /// This enum is containing the last sync result situation
    /// TODO : Implentation needed ?
    /// </summary>
    public enum SyncState
    {
        Successful,
        RollbackBeforeEnsuringScopes,
        RollbackAfterEnsuringScopes,
        RollbackBeforeEnsuringConfiguration,
        RollbackAfterEnsuringConfiguration,
        RollbackBeforeEnsuringDatabase,
        RollbackAfterEnsuringDatabase,
        RollbackBeforeSelectingChanges,
        RollbackAfterSelectedChanges,
        RollbackBeforeApplyingChanges,
        RollbackAfterAppliedChanges,
        UnknownError,
    }
	
	 /// <summary>
    /// Synchronization mode. Could be Normal, Reinitialize, ReinitializeWithUpload
    /// </summary>
    public enum SyncType
    {
        /// <summary>
        /// Normal synchronization
        /// </summary>
        Normal,
        /// <summary>
        /// Reinitialize the whole sync database, applying all rows from the server to the client
        /// </summary>
        Reinitialize,
        /// <summary>
        /// Reinitialize the whole sync database, applying all rows from the server to the client, after trying a client upload
        /// </summary>
        ReinitializeWithUpload
    }

	## DataImport

/// <summary>
    /// Sync progress step. Used for the user feedback
    /// </summary>
    public enum SyncStage
    {
        None,
        BeginSession,
        ScopeLoading,
        ScopeSaved,

        SchemaReading,
        SchemaRead,
        SchemaApplying,
        SchemaApplied,
        TableSchemaApplying,
        TableSchemaApplied,

        TableChangesSelecting,
        TableChangesSelected,

        DatabaseChangesApplying,
        DatabaseChangesApplied,
        TableChangesApplying,
        TableChangesApplied,

        EndSession,
        CleanupMetadata
    }
	
	/// <summary>
    /// This enum is containing the last sync result situation
    /// TODO : Implentation needed ?
    /// </summary>
    public enum SyncState
    {
        Successful,
        RollbackBeforeEnsuringScopes,
        RollbackAfterEnsuringScopes,
        RollbackBeforeEnsuringConfiguration,
        RollbackAfterEnsuringConfiguration,
        RollbackBeforeEnsuringDatabase,
        RollbackAfterEnsuringDatabase,
        RollbackBeforeSelectingChanges,
        RollbackAfterSelectedChanges,
        RollbackBeforeApplyingChanges,
        RollbackAfterAppliedChanges,
        UnknownError,
    }
	
	 /// <summary>
    /// Synchronization mode. Could be Normal, Reinitialize, ReinitializeWithUpload
    /// </summary>
    public enum SyncType
    {
        /// <summary>
        /// Normal synchronization
        /// </summary>
        Normal,
        /// <summary>
        /// Reinitialize the whole sync database, applying all rows from the server to the client
        /// </summary>
        Reinitialize,
        /// <summary>
        /// Reinitialize the whole sync database, applying all rows from the server to the client, after trying a client upload
        /// </summary>
        ReinitializeWithUpload
    }

	
## Generics

https://proj.org/index.html

encoding 
https://gist.github.com/TaoK/945127
http://architectshack.com/TextFileEncodingDetector.ashx

All dates to DateTimeOffset, create samples
Sqlite automated backup
Sqlite database extension TodoSQLite..sqlite3
Register your mobile application with the server
server|client always https



public static class TaskExtensions
{
    // NOTE: Async void is intentional here. This provides a way
    // to call an async method from the constructor while
    // communicating intent to fire and forget, and allow
    // handling of exceptions
    public static async void SafeFireAndForget(this Task task,
        bool returnToCallingContext,
        Action<Exception> onException = null)
    {
        try
        {
            await task.ConfigureAwait(returnToCallingContext);
        }

        // if the provided action is not null, catch and
        // pass the thrown exception
        catch (Exception ex) when (onException != null)
        {
            onException(ex);
        }
    }
}