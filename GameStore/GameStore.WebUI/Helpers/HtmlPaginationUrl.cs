using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Helpers
{
    public static class HtmlPaginationUrl
    {
        public static string GetPageUrlForNumber(string url, string currentPageParamName, int number)
        {
            string newUrl;

            if (url.Contains(currentPageParamName))
            {
                var newString = String.Format("{0}={1}", currentPageParamName, number);
                newUrl = Regex.Replace(url, String.Format("{0}=[0-9]*", currentPageParamName), newString);
            }
            else
            {
                newUrl = String.Format(url.Contains("?") ? "{0}&{1}={2}" : "{0}?{1}={2}", url, currentPageParamName, number);
            }

            return newUrl;
        }
    }
}