using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GeolocationSample
{
    public class PlatformGeolocation : IPlatformGeolocation
    {
        public async Task<GeolocationDto> Get()
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
                    NOTE: ID_CAP_LOCATION access denied, if location is disabled on tthe device. Example windows
                */

                Console.WriteLine(ex); // TODO: logger
                result.Error = ex.Message;
            }
            result.AcquireDuration = (DateTime.UtcNow - start).ToString();
            return result;
        }

        Task<GeolocationDto> IPlatformGeolocation.Get()
        {
            throw new NotImplementedException();
        }
    }
}
