using System;

namespace GameStore.BLL.Logging
{
    public interface ILogger
    {
        void Debug(string message);
        void Info(string message);
        void Trace(string message);
        void Error(Exception ex);
        void Error(Exception ex, string controllerName, string actionName);
        void Fatal(Exception ex, string additionalMessage = "");
    }
}
