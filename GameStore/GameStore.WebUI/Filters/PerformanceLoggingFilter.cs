using System;
using System.Diagnostics;
using System.Web.Mvc;
using GameStore.BLL.Logging;
using GameStore.BLL.Interfaces;
using Ninject;

namespace GameStore.WebUI.Filters
{
    public class PerformanceLoggingFilter : ActionFilterAttribute
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        [Inject]
        public ILogger Logger { get; set; }


        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            _stopwatch.Start();
        }

        public override void OnActionExecuted(ActionExecutedContext actionContext)
        {
            _stopwatch.Stop();

            var controller = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var method = actionContext.ActionDescriptor.ActionName;
            var time = _stopwatch.ElapsedMilliseconds;
            var address = actionContext.HttpContext.Request.UserHostAddress;

            var message =
                String.Format("Controller: '{2}'. Method: '{1}'. User IP: {3}. Execution time: {0} milliseconds. ",
                    time, method, controller, address);

            Logger.Debug(message);
        }
    }
}