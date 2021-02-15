
## Tasks to learn


## UWP sign package example
https://docs.microsoft.com/en-us/windows-hardware/drivers/dashboard/get-a-code-signing-certificate
https://stackoverflow.com/questions/329396/what-happens-when-a-code-signing-certificate-expires
http://www.cacert.org/
https://comodosslstore.com/code-signing
https://docs.microsoft.com/en-us/windows-hardware/drivers/install/authenticode
https://github.com/xamarin/XamarinCommunityToolkit


## Xamarin to read, samples to read & research

string format string
collapsible label sample add
url add http prefix and sample
Entry show clear button
Make button square, without require Height|Width
swipe action to remove items right or left is default?
home screen (splash fickers android emulator) possible shown multiple times. check on default project or google that

material design apply globally
xamarin forms entry floating label (create sample within material design)
	https://github.com/vecalion/FloatingLabels
	https://material.io/
	
URL find samples
url entry form example
https://dotnetfiddle.net/XduN3A
https://stackoverflow.com/questions/7578857/how-to-check-whether-a-string-is-a-valid-http-url

validation sample
	phone, mobile (use Telephone keyboard)
	    // https://stackoverflow.com/questions/5066329/regex-for-valid-international-mobile-phone-number
		RuleFor(customer => customer.Surname).Matches("^\+[1-9]{1}[0-9]{3,14}$");
	email (Email keyboard)
		RuleFor(customer => customer.Email).EmailAddress();
	URL (Url keyboard)
		RuleFor(x => x.SomeUri).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).When(x => !string.IsNullOrEmpty(x.SomeUri));


Focus & Keyboard On new item set focus and show keyboard, see android contacts
	On edit item set cursorPosition but not show keyboard
	https://dgatto.com/posts/2020/01/hiding-soft-keyboard/


default sample review
	Flyout template
	Tabbed



+swipe action to remove items right or left is default? Left
+home screen (splash fickers android emulator) possible shown multiple times. check on default project or google that. Works well on splash sample

Images and Icons
Sample for all platforms, iOS, android, uwp, have all icons required for the app
	create sample images
	read documentation

CollectionView
	Grouping
	Multi delete, use can select delete action from top menu
		that shall show delete column for each list item
		new menu shall show only delete button,
		back button reset the view back
	See Phone Contacts

skia sharp touch not working on iOS


## Xamarin Commands permission (Role-Profile-Readme.txt) samples to read & research
At first list functionality
	pages, buttons and other
commands|Menu items hide if no rights
	project hide create, edit, delete actions (with edit rule),import,export
		ItemAddUpdateDelete
		ItemActivateDeactivate
	Enable|Disable
	ADD|Edit
	each action
	Delete (need soft delete)
	all project or site (read only)
	possible disable whole pages
		Import
		Export
show|hide buttons
show|hide menu items
show|hide grid actions
show|hide create button
Read only
 can't edit Items, site, tags, jobs
 can't data import
 cant share (what?)
 Server
	can read  Items, site, tags, jobs
	can create workspace items
enable edit|delete|deactivate and other action
	have profiles for that, controlled from server side
	this might not be visisble for the user if server integration
control app from serverside what is available for the UI
	actions
	pages
	navigation
server side settings (key-value) JSON
MenuItem from class instead of xaml

## Speech
speach to text sample (offline) there are not much at a moment

##localization
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


## Android keytool

+Create Android Sign Key
+	C:\Program Files (x86)\Java\jre1.8.0_261\bin
+	keytool -genkey -keyalg RSA -keysize 4096 -validity 36000 -storepass Pass@word1 -v -keystore dev-key.jks -alias dev-key

## Performance
review all layout use just grids and stacklayout
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/deploy-test/performance

## Android defaults
prefere external


## Next
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/
https://github.com/dotnet-architecture/News
https://dotnet.microsoft.com/learn/dotnet/architecture-guides

review
https://github.com/Daddoon/BlazorMobile
https://github.com/XLabs/Xamarin-Forms-Labs

 // TODO:
    public static class DynamicSqlExtensions
    {
        private const string OrderBySq = "ORDER BY";
        private const string Desc = "DESC";
        private const string Asc = "ASC";

        //Expression<Func<T, U>> orderExpr

        public static string OrderBy(this string sql, string propertyName)
        {
            if (sql.IndexOf(OrderBySq, StringComparison.OrdinalIgnoreCase) < 0)
            {
                sql = $"{sql} {OrderBySq}";
            }

            return $"{sql} {propertyName} {Asc}";
        }

        public static string OrderByDescending(this string sql, string propertyName)
        {
            if(sql.IndexOf(OrderBySq, StringComparison.OrdinalIgnoreCase) < 0)
            {
                sql = $"{sql} {OrderBySq}";
            }

            return $"{sql} {propertyName} {Desc}";
        }

        public static string ThenByDescending(this string sql, string propertyName)
        {
            return $"{sql}, {propertyName} {Desc}";
        }

        public static string ThenBy(this string sql, string propertyName)
        {
            return $"{sql}, {propertyName} {Asc}";
        }

        public static string Skip(this string sql, int n)
        {
            return $"{sql} offset {n}";

            //limit 2, 1;
        }

        public static string Take(this string sql, int n)
        {
            return $"{sql} limit {n}";
        }
    }