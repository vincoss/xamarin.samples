
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
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/data-cloud/web-services/

review
https://github.com/Daddoon/BlazorMobile
https://github.com/bramborman/UWPHelper 

## Temp

all database calls catch (DocumentClientException ex)

// file path
 string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.txt");
 
 // database
 var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
 
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