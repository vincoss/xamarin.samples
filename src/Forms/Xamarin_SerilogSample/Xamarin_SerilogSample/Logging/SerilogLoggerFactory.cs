using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace Xamarin_SerilogSample.Logging
{
    public class SerilogLoggerFactory : InbuiltLoggerFactory
    {
        public SerilogLoggerFactory()
        {
            // Setup in here
        }

        public InbuiltLogger CreateLogger(Type type)
        {
            var logger = Log.ForContext(type);
            return new SerilogLogger(logger);
        }
    }
}
