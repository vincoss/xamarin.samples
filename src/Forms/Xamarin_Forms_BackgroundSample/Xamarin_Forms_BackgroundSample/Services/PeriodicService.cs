using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Matcha.BackgroundService;


namespace Xamarin_Forms_BackgroundSample.Services
{
    public class PeriodicService : IPeriodicTask
    {
        public static readonly IList<DateTime> Runs = new List<DateTime>();

        public TimeSpan Interval => TimeSpan.FromSeconds(5);

        public Task<bool> StartJob()
        {
            Runs.Add(DateTime.Now);
            return Task.FromResult(true);
        }
    }
}
