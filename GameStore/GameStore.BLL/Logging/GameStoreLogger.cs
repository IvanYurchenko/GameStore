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
            var message = String.Format("'{0}':'{1}'.\r\nUser IP: {2}. Execution time: {3} milliseconds.\r\n",
                controller,
                action, userIp,
                executionTime);
            _logger.Debug(message);
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
            _logger.Error("Error message: {0}.\r\n", ex.Message);
        }

        public void Error(Exception ex, string controllerName, string actionName)
        {
            _logger.Error("'{1}':'{2}'.\r\nException: {0}.\r\n", ex, controllerName, actionName);
        }

        public void Fatal(Exception ex, string additionalMessage = "")
        {
            _logger.Fatal("{1}\r\nFatal error: '{0}'.\r\nStack Trace: '{2}'\r\n", additionalMessage, ex.Message,
                ex.StackTrace);
        }
    }
}