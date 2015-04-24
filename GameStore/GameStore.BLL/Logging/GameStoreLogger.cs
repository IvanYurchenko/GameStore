using System;
using GameStore.BLL.Interfaces;
using NLog;

namespace GameStore.BLL.Logging
{
    public class GameStoreLogger : ILogger
    {
        private readonly Logger _logger;

        public GameStoreLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Debug(string controller, string action, string userIp, long executionTime)
        {
            _logger.Debug("'{0}':'{1}'.{4}User IP: {2}. Execution time: {3} milliseconds. ", controller, action, userIp,
                executionTime,
                Environment.NewLine);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        public void Error(Exception ex)
        {
            _logger.Error("Error message: {0}.", ex.Message);
        }

        public void Error(Exception ex, string controllerName, string actionName)
        {
            _logger.Error("'{1}':'{2}'.\r\nError message: {0}.", ex.Message, controllerName, actionName);
        }

        public void Fatal(Exception ex, string additionalMessage = "")
        {
            _logger.Fatal("{1}\r\nFatal error message: '{0}'.\r\nStack Trace: '{2}'", additionalMessage, ex.Message,
                ex.StackTrace);
        }
    }
}