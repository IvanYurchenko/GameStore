using System.Globalization;
using System.Threading;
using System.Web.Http;

namespace GameStore.WebUI.ApiControllers
{
    public class BaseApiController : ApiController
    {
        public const string DefaultAction = "DefaultAction";

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            if (controllerContext.RouteData.Values["lang"] != null && controllerContext.RouteData.Values["lang"] as string != "null")
            {
                var currentLangCode = controllerContext.RouteData.Values["lang"] as string;
                try
                {
                    var ci = new CultureInfo(currentLangCode);
                    Thread.CurrentThread.CurrentUICulture = ci;
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
                }
                catch (CultureNotFoundException ex)
                {
                }
            }

            base.Initialize(controllerContext);
        }
    }
}
