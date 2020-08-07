using System;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Essentials_GeolocationView : ContentPage
    {
        public Essentials_GeolocationView()
        {
            InitializeComponent();

            Show();
        }

        public async void Show()
        {
            await GetLastLocation();
            await GetLocation();

            var dto = await GetLocationMethod();
            lblInfo2.Text = dto.ToString();
        }

        public async Task GetLastLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            DisplayLocationInfo(lstLocation, location, true);
        }

        public async Task GetLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(request);
            DisplayLocationInfo(lstLocationBest, location);
            lblInfo.Text = location.ToString();
        }

        public void DisplayLocationInfo(ListView listView, Location location, bool isLastLocation = false)
        {
            if (location == null)
            {
                return;
            }

            try
            {
                var items = new[]
                     {
                        $"IsLastLocation:       {isLastLocation}",
                        $"Accuracy:             {location.Accuracy}",
                        $"Altitude:             {location.Altitude}",
                        $"Course:               {location.Course}",
                        $"IsFromMockProvider:   {location.IsFromMockProvider}",
                        $"Latitude:             {location.Latitude}",
                        $"Longitude:            {location.Longitude}",
                        $"Speed:                {location.Speed}",
                        $"Timestamp:            {location.Timestamp}",
                    };

                listView.ItemsSource = items;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                listView.ItemsSource = new[] { fnsEx.Message };
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                listView.ItemsSource = new[] { fneEx.Message };
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                listView.ItemsSource = new[] { pEx.Message };
            }
            catch (Exception ex)
            {
                // Unable to get 
                listView.ItemsSource = new[] { ex.Message };
            }
        }

        private async Task<GeolocationDto> GetLocationMethod()
        {
            // TODO: try catch
            var result = new GeolocationDto();
            var loc = await Geolocation.GetLastKnownLocationAsync();
            if (loc == null)
            {
                // potential long running method
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
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

            return result;
        }

        public Task<DeviceInfoDto> GetDeviceInfo()
        {
            var result = new DeviceInfoDto
            {
                Model = DeviceInfo.Model,
                Manufacturer = DeviceInfo.Manufacturer,
                Version = DeviceInfo.VersionString
            };
            return Task.FromResult(result);
        }

        class GeolocationDto
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public double? Altitude { get; set; }
            public double? Accuracy { get; set; }
            public double? VerticalAccuracy { get; set; }
            public double? Speed { get; set; }
            public double? Course { get; set; }
            public DateTimeOffset Timestamp { get; set; }
            public bool IsMock { get; set; }

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

        public class DeviceInfoDto
        {
            public string Model { get; set; }
            public string Manufacturer { get; set; }
            public string Version { get; set; }
        }
    }
}