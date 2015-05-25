using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult AuthorizedOnly()
        {
            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
