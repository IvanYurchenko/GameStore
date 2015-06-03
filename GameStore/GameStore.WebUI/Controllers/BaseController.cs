using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using GameStore.WebUI.BootstrapSupport;

namespace GameStore.WebUI.Controllers
{
    public class BaseController: Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.RouteData.Values["lang"] != null && requestContext.RouteData.Values["lang"] as string != "null")
            {
                var currentLangCode = requestContext.RouteData.Values["lang"] as string;
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

            base.Initialize(requestContext);
        }
        
        /// <summary>
        /// Adds current message as an attention message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void MessageAttention(string message)
        {
            TempData.Add(Alerts.ATTENTION, message);
        }

        /// <summary>
        /// Adds current message as a success message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void MessageSuccess(string message)
        {
            TempData.Add(Alerts.SUCCESS, message);
        }

        /// <summary>
        /// Adds current message as an information message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void MessageInformation(string message)
        {
            TempData.Add(Alerts.INFORMATION, message);
        }

        /// <summary>
        /// Adds current message as an error message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void MessageError(string message)
        {
            TempData.Add(Alerts.ERROR, message);
        }
    }
}
