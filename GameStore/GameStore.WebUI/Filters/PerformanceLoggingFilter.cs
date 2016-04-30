using System.Diagnostics;
using System.Web.Mvc;
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

            string controller = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string action = actionContext.ActionDescriptor.ActionName;
            long time = _stopwatch.ElapsedMilliseconds;
            string ip = actionContext.HttpContext.Request.UserHostAddress;

            Logger.Debug(controller, action, ip, time);
        }
    }
}