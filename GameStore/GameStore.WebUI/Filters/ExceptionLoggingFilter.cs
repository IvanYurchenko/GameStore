using System.Web.Mvc;
using BootstrapSupport;
using GameStore.BLL.Interfaces;
using Ninject;

namespace GameStore.WebUI.Filters
{
    public class ExceptionLoggingFilter : ActionFilterAttribute, IExceptionFilter
    {
        [Inject]
        public ILogger Logger { get; set; }

        public void OnException(ExceptionContext filterContext)
        {
            var controllerName = (string) filterContext.RouteData.Values["controller"];
            var actionName = (string) filterContext.RouteData.Values["action"];

            Logger.Error(filterContext.Exception, controllerName, actionName);

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            if (!filterContext.Controller.TempData.ContainsKey(Alerts.ERROR))
            {
                filterContext.Controller.TempData.Add(Alerts.ERROR, "An error has occured. Please try again later.");
            }
            filterContext.HttpContext.Response.StatusCode = 500;

            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            filterContext.Result = new RedirectResult("/");
        }
    }
}