using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace GameStore.WebUI.Helpers
{
    public static class LanguageHelper
    {
        public static MvcHtmlString LanguageSelectorLink(this HtmlHelper helper,
         string lang, string text)
        {
            var routeValues = new RouteValueDictionary(helper.ViewContext.RouteData.Values);

            NameValueCollection queryString = helper.ViewContext.HttpContext.Request.QueryString;

            foreach (string key in queryString)
            {
                if (queryString[key] != null && !string.IsNullOrEmpty(key))
                {
                    if (routeValues.ContainsKey(key))
                    {
                        routeValues[key] = queryString[key];
                    }
                    else
                    {
                        routeValues.Add(key, queryString[key]);
                    }
                }
            }

            routeValues["lang"] = lang.ToLower();

            var liTagBuilder = new TagBuilder("li");
            var aTagBuilder = new TagBuilder("a");
            var routeValueDictionary = new RouteValueDictionary(helper.ViewContext.RouteData.Values);

            if (routeValueDictionary.ContainsKey("lang"))
            {
                if (helper.ViewContext.RouteData.Values["lang"] as string == lang)
                {
                    liTagBuilder.AddCssClass("active");
                }
                else
                {
                    routeValueDictionary["lang"] = lang;
                }
            }

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            aTagBuilder.MergeAttribute("href", urlHelper.RouteUrl(routeValues));
            aTagBuilder.SetInnerText(text);

            liTagBuilder.InnerHtml = aTagBuilder.ToString();
            return new MvcHtmlString(liTagBuilder.ToString());
        }
    }
}