using System.Web.Mvc;
using BootstrapSupport;

namespace BootstrapMvcSample.Controllers
{
    public class BootstrapBaseController: Controller
    {
        public void MessageAttention(string message)
        {
            TempData.Add(Alerts.ATTENTION, message);
        }

        public void MessageSuccess(string message)
        {
            TempData.Add(Alerts.SUCCESS, message);
        }

        public void MessageInformation(string message)
        {
            TempData.Add(Alerts.INFORMATION, message);
        }

        public void MessageError(string message)
        {
            TempData.Add(Alerts.ERROR, message);
        }
    }
}
