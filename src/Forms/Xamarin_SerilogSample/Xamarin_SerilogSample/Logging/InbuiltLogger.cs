using System;
using System.Threading;


namespace Xamarin_SerilogSample.Logging
{
    public enum InbuiltLogLevel : byte
    {
        Debug,
        Information,
        Warning,
        Error,
        Fatal
    }
    
    public interface InbuiltLogger
    {
        bool IsEnabled(InbuiltLogLevel level);

        void Log(InbuiltLogLevel level, Exception exception, string format, params object[] args);
    }

    public class InbuiltConsoleLogger : InbuiltLogger
    {
        public bool IsEnabled(InbuiltLogLevel level)
        {
            return true;
        }

        public void Log(InbuiltLogLevel level, Exception exception, string format, params object[] args)
        {
            string message = format;
            if (args != null && args.Length > 0)
            {
                message = string.Format(format, args);
            }

            string log = string.Format("{0} [{1}] {2} {3}", DateTime.Now.ToString("hh:mm:ss.ffff tt"), Thread.CurrentThread.ManagedThreadId, level, message);

            Console.WriteLine(log);

            if (exception != null)
            {
                Console.WriteLine(exception);
            }
        }
    }

    public interface InbuiltLoggerFactory
    {
        InbuiltLogger CreateLogger(Type type);
    }

    public class InbuiltConsoleLoggerFactory : InbuiltLoggerFactory
    {
        private static readonly InbuiltLogger Logger = new InbuiltConsoleLogger();

        public InbuiltLogger CreateLogger(Type type)
        {
            return Logger;
        }
    }

    public sealed class InbuiltLog
    {
        private static InbuiltLoggerFactory _factory;

        static InbuiltLog()
        {
            _factory = new InbuiltNullLoggerFactory();
        }

        #region For

        public static InbuiltLogger For(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            return GetLogger(type);
        }

        public static InbuiltLogger For(object itemThatRequiresLoggingServices)
        {
            if (itemThatRequiresLoggingServices == null)
            {
                throw new ArgumentNullException(nameof(itemThatRequiresLoggingServices));
            }
            return For(itemThatRequiresLoggingServices.GetType());
        }

        public static void SetFactory(InbuiltLoggerFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            _factory = factory;
        }

        #endregion

        #region GetLogger

        private static InbuiltLogger GetLogger(Type type)
        {
            return _factory.CreateLogger(type);
        }
        #endregion
    }

    public static class InbuiltLoggingExtensions
    {
        public static void Debug(this InbuiltLogger logger, string message)
        {
            LogInternal(logger, InbuiltLogLevel.Debug, null, message, null);
        }

        public static void Debug(this InbuiltLogger logger, Exception exception, string message)
        {
            LogInternal(logger, InbuiltLogLevel.Debug, exception, message, null);
        }

        public static void Debug(this InbuiltLogger logger, string format, params object[] args)
        {
            LogInternal(logger, InbuiltLogLevel.Debug, null, format, args);
        }

        public static void Debug(this InbuiltLogger logger, Exception exception, string format, params object[] args)
        {
            LogInternal(logger, InbuiltLogLevel.Debug, exception, format, args);
        }

        public static void Error(this InbuiltLogger logger, string message)
        {
            LogInternal(logger, InbuiltLogLevel.Error, null, message, null);
        }

        public static void Error(this InbuiltLogger logger, Exception exception, string message)
        {
            LogInternal(logger, InbuiltLogLevel.Error, exception, message, null);
        }

        public static void Error(this InbuiltLogger logger, string format, params object[] args)
        {
            LogInternal(logger, InbuiltLogLevel.Error, null, format, args);
        }

        public static void Error(this InbuiltLogger logger, Exception exception, string format, params object[] args)
        {
            LogInternal(logger, InbuiltLogLevel.Error, exception, format, args);
        }

        public static void Warning(this InbuiltLogger logger, string message)
        {
            LogInternal(logger, InbuiltLogLevel.Warning, null, message, null);
        }

        public static void Info(this InbuiltLogger logger, string message)
        {
            LogInternal(logger, InbuiltLogLevel.Information, null, message, null);
        }

        #region Private methods

        private static void LogInternal(InbuiltLogger logger, InbuiltLogLevel level, Exception exception, string format, object[] objects)
        {
            if (logger.IsEnabled(level))
            {
                logger.Log(level, exception, format, objects);
            }
        }

        #endregion
    }

    public class InbuiltNullLogger : InbuiltLogger
    {
        public bool IsEnabled(InbuiltLogLevel level)
        {
            return false;
        }

        public void Log(InbuiltLogLevel level, Exception exception, string format, params object[] args)
        {
            // Hola
        }
    }

    public class InbuiltNullLoggerFactory : InbuiltLoggerFactory
    {
        private static readonly InbuiltLogger Logger = new InbuiltNullLogger();

        public InbuiltLogger CreateLogger(Type type)
        {
            return Logger;
        }
    }
}
