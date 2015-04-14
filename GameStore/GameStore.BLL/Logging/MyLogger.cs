using System;
using NLog;

namespace GameStore.BLL.Logging
{
    public class MyLogger : ILogger
    {
        private Logger _logger;

        public MyLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Debug(string message)
        {
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
            _logger.Error("Error message: {0}.", ex.Message);
        }

        public void Error(Exception ex, string controllerName, string actionName)
        {
            _logger.Error("Controller: '{1}'. Method: '{2}'. Error message: {0}.", ex.Message, controllerName, actionName);
        }

        public void Fatal(Exception ex, string additionalMessage = "")
        {
            _logger.Fatal("{1} Fatal error message: '{0}'. Stack Trace: '{2}'", additionalMessage, ex.Message, ex.StackTrace);
        }
    }
}
