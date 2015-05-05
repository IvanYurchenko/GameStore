using System.Web.Mvc;
using GameStore.WebUI.BootstrapSupport;

namespace BootstrapMvcSample.Controllers
{
    public class BaseController: Controller
    {
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
