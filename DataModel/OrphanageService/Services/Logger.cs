using Serilog;

using System;

namespace OrphanageService.Services
{
    public class Logger : Interfaces.ILogger
    {
        private readonly Serilog.Core.Logger _log;

        /// <inheritdoc />
        public Logger()
        {
            _log = new LoggerConfiguration().MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File($"ServiceLog.txt",
                fileSizeLimitBytes: 10485760,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                shared: true)
                .CreateLogger();

            _log.Information("Logging initialized.");
        }

        /// <inheritdoc />
        public void Debug(string messageTemplate)
        {
            _log.Debug(messageTemplate);
        }

        /// <inheritdoc />
        public void Information(string messageTemplate)
        {
            _log.Information(messageTemplate);
        }

        /// <inheritdoc />
        public void Warning(string messageTemplate)
        {
            _log.Warning(messageTemplate);
        }

        /// <inheritdoc />
        public void Error(string messageTemplate)
        {
            _log.Error(messageTemplate);
        }

        /// <inheritdoc />
        public void Fatal(string messageTemplate)
        {
            _log.Fatal(messageTemplate);
        }

        /// <inheritdoc />
        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            _log.Debug(messageTemplate, propertyValues);
        }

        /// <inheritdoc />
        public void Information(string messageTemplate, params object[] propertyValues)
        {
            _log.Information(messageTemplate, propertyValues);
        }

        /// <inheritdoc />
        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            _log.Warning(messageTemplate, propertyValues);
        }

        /// <inheritdoc />
        public void Error(string messageTemplate, params object[] propertyValues)
        {
            _log.Error(messageTemplate, propertyValues);
        }

        /// <inheritdoc />
        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
            _log.Fatal(messageTemplate, propertyValues);
        }

        /// <inheritdoc />
        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _log.Debug(exception, messageTemplate, propertyValues);
        }

        /// <inheritdoc />
        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _log.Information(exception, messageTemplate, propertyValues);
        }

        /// <inheritdoc />
        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _log.Warning(exception, messageTemplate, propertyValues);
        }

        /// <inheritdoc />
        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _log.Error(exception, messageTemplate, propertyValues);
        }

        /// <inheritdoc />
        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _log.Fatal(exception, messageTemplate, propertyValues);
        }
    }
}