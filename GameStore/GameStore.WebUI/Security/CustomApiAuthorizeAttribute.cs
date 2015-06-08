using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Routing;

namespace GameStore.WebUI.Security
{
    public class CustomApiAuthorizeAttribute : AuthorizeAttribute
    {
        public string UsersConfigKey { get; set; }

        public string RolesConfigKey { get; set; }

        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }

        public override void OnAuthorization(HttpActionContext filterContext)
        {
            if (CurrentUser != null && CurrentUser.Identity.IsAuthenticated)
            {
                string authorizedUsers = ConfigurationManager.AppSettings[UsersConfigKey];
                string authorizedRoles = ConfigurationManager.AppSettings[RolesConfigKey];

                Users = String.IsNullOrEmpty(Users) ? authorizedUsers : Users;
                Roles = String.IsNullOrEmpty(Roles) ? authorizedRoles : Roles;

                if (!String.IsNullOrEmpty(Roles))
                {
                    if (!CurrentUser.IsInRole(Roles))
                    {
                        filterContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    }
                }

                if (!String.IsNullOrEmpty(Users))
                {
                    if (!Users.Contains(CurrentUser.UserId.ToString()))
                    {
                        filterContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    }
                }
            }
            else
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }
    }
}