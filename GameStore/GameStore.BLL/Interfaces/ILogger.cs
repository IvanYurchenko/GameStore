using System;

namespace GameStore.BLL.Interfaces
{
    public interface ILogger
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Debug(string message);

        /// <summary>
        /// Logs the specified values.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <param name="userIp">The user ip.</param>
        /// <param name="executionTime">The execution time.</param>
        void Debug(string controller, string action, string userIp, long executionTime);

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(string message);

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Trace(string message);

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void Error(Exception ex);

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="actionName">Name of the action.</param>
        void Error(Exception ex, string controllerName, string actionName);

        /// <summary>
        /// Logs the specified exception with stacktrace.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="additionalMessage">The additional message.</param>
        void Fatal(Exception ex, string additionalMessage = "");
    }
}