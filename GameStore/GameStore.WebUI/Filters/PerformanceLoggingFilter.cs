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

            var controller = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var action = actionContext.ActionDescriptor.ActionName;
            var time = _stopwatch.ElapsedMilliseconds;
            var ip = actionContext.HttpContext.Request.UserHostAddress;

            Logger.Debug(controller, action, ip, time);
        }
    }
}