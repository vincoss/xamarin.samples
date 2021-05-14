
## Tasks to learn


## UWP sign package example
https://docs.microsoft.com/en-us/windows-hardware/drivers/dashboard/get-a-code-signing-certificate
https://stackoverflow.com/questions/329396/what-happens-when-a-code-signing-certificate-expires
http://www.cacert.org/
https://comodosslstore.com/code-signing
https://docs.microsoft.com/en-us/windows-hardware/drivers/install/authenticode
https://github.com/xamarin/XamarinCommunityToolkit


## geolocation
create a looper service that will always try to get best location, then we use that one get get from

 public class GeolocationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Altitude { get; set; }
        public double? Accuracy { get; set; }
        public double? VerticalAccuracy { get; set; }
        public double? Speed { get; set; }
        public double? Course { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string AcquireDuration { get; set; }
        public bool IsMock { get; set; }
        public string Error { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"{nameof(Latitude)}: {Latitude}, ");
            sb.Append($"{nameof(Longitude)}: {Longitude}, ");
            sb.Append($"{nameof(Altitude)}: {Altitude ?? 0}, ");
            sb.Append($"{nameof(Accuracy)}: {Accuracy ?? 0}, ");
            sb.Append($"{nameof(VerticalAccuracy)}: {VerticalAccuracy ?? 0}, ");
            sb.Append($"{nameof(Speed)}: {Speed ?? 0}, ");
            sb.Append($"{nameof(Course)}: {Course ?? 0}, ");
            sb.Append($"{nameof(Timestamp)}: {Timestamp}, ");
            sb.Append($"{nameof(IsMock)}: {IsMock}");

            return sb.ToString();
        }
    }

public async Task<GeolocationDto> GetLocationMethodAsync()
        {
            var start = DateTime.UtcNow;
            var result = new GeolocationDto();
            try
            {
                var loc = await Geolocation.GetLastKnownLocationAsync();
                if (loc == null)
                {
                    // potential long running method
                    var request = new GeolocationRequest(GeolocationAccuracy.Best)
                    {
                        Timeout = TimeSpan.FromSeconds(10)
                    };
                    loc = await Geolocation.GetLocationAsync(request);
                }

                if (loc != null)
                {
                    result.Latitude = loc.Latitude;
                    result.Longitude = loc.Longitude;
                    result.Altitude = loc.Altitude;
                    result.Accuracy = loc.Accuracy;
                    result.VerticalAccuracy = loc.VerticalAccuracy;
                    result.Speed = loc.Speed;
                    result.Course = loc.Course;
                    result.Timestamp = loc.Timestamp;
                    //https://stackoverflow.com/questions/42569245/detect-or-prevent-if-user-uses-fake-location
                    result.IsMock = loc.IsFromMockProvider;
                }
            }
            catch (Exception ex)
            {
                /*
                    NOTE: ID_CAP_LOCATION access denied, if location is disabled on tthe device.
                */

                Logger.Error(ex, nameof(GetLocationMethodAsync));
                result.Error = ex.Message;
            }
            result.AcquireDuration = (DateTime.UtcNow - start).ToString();
            return result;
        }

## Xamarin to read, samples to read & research




see TinyIoC
	https://github.com/grumpydev/TinyIoC
Finish the Dapper Sqlite sample provider samples, see fluentmigration with dapper
signature update full screen
Finish the Dapper Sqlite provider samples
create address entry form, try to use also GPS picker to get location
TableView for forms, create sample, review and update existing

home screen (splash fickers android emulator) possible shown multiple times. check on default project or google that



validation sample
	phone, mobile (use Telephone keyboard)
	    // https://stackoverflow.com/questions/5066329/regex-for-valid-international-mobile-phone-number
		RuleFor(customer => customer.Surname).Matches("^\+[1-9]{1}[0-9]{3,14}$");
	email (Email keyboard)
		RuleFor(customer => customer.Email).EmailAddress();
	URL (Url keyboard)
		RuleFor(x => x.SomeUri).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).When(x => !string.IsNullOrEmpty(x.SomeUri));




default sample review
	Flyout template
	Tabbed

+review Entry_Focus each platform
+Focus & Keyboard On new item set focus and show keyboard, see android contacts
+swipe action to remove items right or left is default? (Left to delete)
+floating label entry
+square button
+url add http prefix and sample
+swipe action to remove items right or left is default? Left
+home screen (splash fickers android emulator) possible shown multiple times. check on default project or google that. Works well on splash sample

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

## Android defaults
prefere external

## Speech
+speach to text sample (offline) there are not much at a moment

## Android keytool

+Create Android Sign Key
+	C:\Program Files (x86)\Java\jre1.8.0_261\bin
+	keytool -genkey -keyalg RSA -keysize 4096 -validity 36000 -storepass Pass@word1 -v -keystore dev-key.jks -alias dev-key

## Performance
review all layout use just grids and stacklayout
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/deploy-test/performance


## Next
https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/
https://github.com/dotnet-architecture/News
https://dotnet.microsoft.com/learn/dotnet/architecture-guides

review
https://github.com/Daddoon/BlazorMobile
https://github.com/XLabs/Xamarin-Forms-Labs


      

  