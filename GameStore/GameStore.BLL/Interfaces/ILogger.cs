using System;

namespace GameStore.BLL.Interfaces
{
    public interface ILogger
    {
        void Debug(string message);
        void Debug(string controller, string action, string userIp, long executionTime);
        void Info(string message);
        void Trace(string message);
        void Error(Exception ex);
        void Error(Exception ex, string controllerName, string actionName);
        void Fatal(Exception ex, string additionalMessage = "");
    }
}