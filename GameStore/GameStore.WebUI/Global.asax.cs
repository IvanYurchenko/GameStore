using System;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using AutoMapper;
using GameStore.WebUI.Mappings;
using GameStore.WebUI.Security;
using Newtonsoft.Json;

namespace GameStore.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Bundles
            BootstrapBundleConfig.RegisterBundles(BundleTable.Bundles);

            // Mapper
            Mapping.MapInit();
            Mapper.AssertConfigurationIsValid();
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var serializeModel =
                    JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);

                var newUser = new CustomPrincipal(authTicket.Name)
                {
                    UserId = serializeModel.UserId,
                    FirstName = serializeModel.FirstName,
                    LastName = serializeModel.LastName,
                    Roles = serializeModel.Roles
                };

                HttpContext.Current.User = newUser;
            }
        }

        private void SessionStart(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Add("__MyAppSession", string.Empty);
        }
    }
}