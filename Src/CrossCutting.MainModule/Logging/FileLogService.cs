using System;
using System.IO;
using log4net;
using CrossCutting.Core.Logging;

namespace CrossCutting.MainModule.Logging
{
    public sealed class FileLogService : ILogService
    {
        readonly ILog _logger;

        static FileLogService()
        {
            var log4NetConfigDirectory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;

            var log4NetConfigFilePath = Path.Combine(log4NetConfigDirectory, "log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(log4NetConfigFilePath));
        }

        public FileLogService()
        {
            _logger = LogManager.GetLogger(typeof(FileLogService));
        }

        public void Fatal(string errorMessage)
        {
            if (_logger.IsFatalEnabled)
            {
                _logger.Fatal(errorMessage);
            }
        }

        public void Error(string errorMessage)
        {
            if (_logger.IsErrorEnabled)
            {
                _logger.Error(errorMessage);
            }
        }

        public void Warn(string message)
        {
            if (_logger.IsWarnEnabled)
            {
                _logger.Warn(message);
            }
        }

        public void Info(string message)
        {
            if (_logger.IsInfoEnabled)
            {
                _logger.Info(message);
            }
        }

        public void Debug(string message)
        {
            if (_logger.IsDebugEnabled)
            {
                _logger.Debug(message);
            }
        }
    }
}