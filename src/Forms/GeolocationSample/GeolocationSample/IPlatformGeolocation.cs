using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeolocationSample
{
    public interface IPlatformGeolocation
    {
        Task<GeolocationDto> Get();
    }
}
