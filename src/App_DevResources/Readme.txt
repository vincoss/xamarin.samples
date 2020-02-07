
## Tasks to learn


MenuItem from class instead of xaml
viss git logo
font awsome icons samples
image zoon and pan, see gestures
Master view

sync
dropbox
rhiho lic, server, client, verify..., export lic, import lic
canon uwp

GPS UTM
	https://en.wikipedia.org/wiki/Geocode

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

Status
	name
	weight
	color
	
files store Git LFS
server shall add comments and tags also into existing images
server photo galery interval vertical thumpnail preview
DbUp client database upgrade. if client has changes then those changes won't be migrated into new schema.
	possible fail dbUp if client has unsinc changes
	
some apps will never need to sinc then no need to sync to server
some apps will not sync initially but later might need to sync to server. is collect changes not enable then there is nothing to send to the server. ???

server API
https://medium.com/lightrail/getting-token-authentication-right-in-a-stateless-single-page-application-57d0c6474e3
C:\_Data\GitHub\aspnet\AspNetCore.Docs\aspnetcore\mobile
C:\_Data\GitHub\aspnet\AspNetCore.Docs\aspnetcore\mobile\native-mobile-backend\sample\ToDoApi
https://docs.microsoft.com/en-us/aspnet/core/mobile/native-mobile-backend?view=aspnetcore-3.1
https://docs.microsoft.com/en-us/archive/msdn-magazine/2016/august/asp-net-core-real-world-asp-net-core-mvc-filters
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/authentication-and-authorization

compiler warning to errors settings
camera & mic setup in small room, wire through the door on window side
wifi router for shed

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

	## DataImport

	
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

errors
	deleted
	validation
	unknown
	
https://github.com/i-e-b/DiskQueue/tree/master/src/DiskQueue/Implementation
https://josefottosson.se/asp-net-core-protect-your-api-with-api-keys/
