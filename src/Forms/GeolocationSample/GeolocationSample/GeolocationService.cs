﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GeolocationSample
{
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
            sb.Append($"{nameof(AcquireDuration)}: {AcquireDuration},");
            sb.Append($"{nameof(IsMock)}: {IsMock},");
            sb.Append($"{nameof(Error)}: {Error}");

            return sb.ToString();
        }
    }

    public interface IGeolocationService
    {
        Task<GeolocationDto> Get2();
    }

    // Note register as singleton
    public class GeolocationService : IGeolocationService
    {
        private Location _location;
        private CompassData _compassData;
        private TimeSpan? _acquireDuration;
        private long _last = DateTime.UtcNow.Ticks;
        private static long _intervalTicks = TimeSpan.FromSeconds(1).Ticks;

        private void SetupCompass()
        {
            try
            {
                if (Compass.IsMonitoring)
                {
                    return;
                }

                Compass.ReadingChanged += Compass_ReadingChanged;
                Compass.Start(SensorSpeed.Fastest);
            }
            catch // Do not crash
            {

            }
        }

        private void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {
            _compassData = e.Reading;
        }

        private async void Fetch()
        {
            if ((DateTime.UtcNow.Ticks - _last) < _intervalTicks)
            {
                return;
            }

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best)
                {
                    Timeout = TimeSpan.FromSeconds(360)
                };
                _location = await Geolocation.GetLocationAsync(request);
                _acquireDuration = TimeSpan.FromTicks(DateTime.UtcNow.Ticks - _last);
                _last = DateTime.UtcNow.Ticks;
            }
            catch // Do not crash
            {
                _location = null;
                _acquireDuration = null;
            }
        }

        /// <summary>
        /// Periodically fetch best location and store it.
        /// </summary>
        public  void Run()
        {
            SetupCompass();

            // NOTE: timer is used since on UWP task throws.
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Fetch();

                return true; // Repeat
            });
        }

        public async Task<GeolocationDto> Get()
        {
            var start = DateTime.UtcNow;
            var result = new GeolocationDto();
            try
            {
                if (_location == null)
                {
                    // Since there is not cached location, just get lowest one.
                    var request = new GeolocationRequest(GeolocationAccuracy.Lowest)
                    {
                        Timeout = TimeSpan.FromSeconds(30)
                    };
                    var location = await Geolocation.GetLocationAsync(request); 
                    result.AcquireDuration = (DateTime.UtcNow - start).ToString();
                    Map(location, result);
                }
                else
                {
                    result.AcquireDuration = _acquireDuration.ToString();
                    Map(_location, result);
                }
            }
            catch (Exception ex)
            {
                /*
                    NOTE: ID_CAP_LOCATION access denied, if location is disabled on tthe device.
                */

                // Logger.Error(ex, nameof(GetLocationMethodAsync));
                result.Error = ex.Message;
            }
            return result;
        }

        private void Map(Location location, GeolocationDto dto)
        {
            dto.Latitude = location.Latitude;
            dto.Longitude = location.Longitude;
            dto.Altitude = location.Altitude;
            dto.Accuracy = location.Accuracy;
            dto.VerticalAccuracy = location.VerticalAccuracy;
            dto.Speed = location.Speed;
            dto.Course = location.Course;
            dto.Timestamp = location.Timestamp;
            //https://stackoverflow.com/questions/42569245/detect-or-prevent-if-user-uses-fake-location
            dto.IsMock = location.IsFromMockProvider;

            if (dto.Course == null && _compassData.HeadingMagneticNorth > 0)
            {
                dto.Course = _compassData.HeadingMagneticNorth;
            }
        }

        public async Task<GeolocationDto> Get2()
        {
            var start = DateTime.UtcNow;
            var result = new GeolocationDto();
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    // Since there is not cached location, just get one.
                    var request = new GeolocationRequest(GeolocationAccuracy.Best)
                    {
                        Timeout = TimeSpan.FromSeconds(360)
                    };
                    location = await Geolocation.GetLocationAsync(request);
                }
                Map(location, result);
            }
            catch (Exception ex)
            {
                /*
                    NOTE: ID_CAP_LOCATION access denied, if location is disabled on tthe device.
                */

                // Logger.Error(ex, nameof(GetLocationMethodAsync)); // TODO:
                result.Error = ex.Message;
            }
            result.AcquireDuration = (DateTime.UtcNow - start).ToString();
            return result;
        }
    }
}
