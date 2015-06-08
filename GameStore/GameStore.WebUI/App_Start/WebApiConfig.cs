using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Mvc;
using GameStore.Core;

namespace GameStore.WebUI
{
    public static class WebApiConfig
    {
        private const string DefaultLanguage = Constants.EnglishLanguageCode;

        public static void Register(HttpConfiguration config)
        {
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.Add(new XmlMediaTypeFormatter());

            config.Formatters.XmlFormatter.AddQueryStringMapping("type", "xml", "text/xml");
            config.Formatters.JsonFormatter.AddQueryStringMapping("type", "json", "application/json");

            //config.Filters.Add(new ApiLogErrorAttribute());

            config.Routes.MapHttpRoute(
                name: "DetailsApiRoute",
                routeTemplate: "api/{lang}/{controller}/{id}/{action}/{actionId}",
                defaults: new { lang = DefaultLanguage, action = "DefaultAction", actionId = UrlParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "SharedApiRoute",
                routeTemplate: "api/{lang}/{controller}",
                defaults: new { lang = DefaultLanguage }
            );
        }
    }
}