using System;
using System.Collections.Generic;
using System.Text;
using Serilog;


namespace Xamarin_SerilogSample.Logging
{
    public class SerilogLogger : InbuiltLogger
    {
        private readonly ILogger _logger;

        public SerilogLogger(ILogger logger)
        {
            if(logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            _logger = logger;
        }

        public bool IsEnabled(InbuiltLogLevel level)
        {
            switch (level)
            {
                case InbuiltLogLevel.Information:
                    {
                        return _logger.IsEnabled(Serilog.Events.LogEventLevel.Information);
                    }
                case InbuiltLogLevel.Debug:
                    {
                        return _logger.IsEnabled(Serilog.Events.LogEventLevel.Debug);
                    }
                case InbuiltLogLevel.Warning:
                    {
                        return _logger.IsEnabled(Serilog.Events.LogEventLevel.Warning);
                    }
                case InbuiltLogLevel.Error:
                    {
                        return _logger.IsEnabled(Serilog.Events.LogEventLevel.Error);
                    }
                case InbuiltLogLevel.Fatal:
                    {
                        return _logger.IsEnabled(Serilog.Events.LogEventLevel.Fatal);
                    }
            }
            return false;

        }

        public void Log(InbuiltLogLevel level, Exception exception, string format, params object[] args)
        {
            if (IsEnabled(level) == false)
            {
                return;
            }
            switch (level)
            {
                case InbuiltLogLevel.Information:
                    {
                        _logger.Information(exception, format, args);
                        break;
                    }
                case InbuiltLogLevel.Debug:
                    {
                        _logger.Debug(exception, format, args);
                        break;
                    }
                case InbuiltLogLevel.Warning:
                    {
                        _logger.Warning(exception, format, args);
                        break;
                    }
                case InbuiltLogLevel.Error:
                    {
                        _logger.Error(exception, format, args);
                        break;
                    }
                case InbuiltLogLevel.Fatal:
                    {
                        _logger.Fatal(exception, format, args);
                        break;
                    }
            }
        }
    }
}
