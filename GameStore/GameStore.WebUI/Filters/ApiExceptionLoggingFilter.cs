using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;
using GameStore.BLL.Interfaces;
using Ninject;

namespace GameStore.WebUI.Filters
{
    public class ApiExceptionLoggingFilter : ExceptionFilterAttribute
    {
        [Inject]
        public ILogger Logger
        {
            get { return DependencyResolver.Current.GetService(typeof(ILogger)) as ILogger; }
        }

        public override void OnException(HttpActionExecutedContext filterContext)
        {
            Exception exception = filterContext.Exception;

            var httpCode = new HttpException(null, exception).GetHttpCode();
            filterContext.Response = new HttpResponseMessage((HttpStatusCode)httpCode);

            Logger.Error(exception);
        }
    }
}