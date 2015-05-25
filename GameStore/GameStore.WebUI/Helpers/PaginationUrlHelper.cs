using System;
using System.Text.RegularExpressions;

namespace GameStore.WebUI.Helpers
{
    public static class PaginationUrlHelper
    {
        /// <summary>
        /// Gets URL for the page with a specified number.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="currentPageParamName">Current page parameter name in the querystring.</param>
        /// <param name="number">The number of the page.</param>
        /// <returns></returns>
        public static string GetPageUrlForNumber(string url, string currentPageParamName, int number)
        {
            string newUrl;

            if (url.Contains(currentPageParamName))
            {
                string newString = String.Format("{0}={1}", currentPageParamName, number);
                newUrl = Regex.Replace(url, String.Format("{0}=[0-9]*", currentPageParamName), newString);
            }
            else
            {
                newUrl = String.Format(url.Contains("?") ? "{0}&{1}={2}" : "{0}?{1}={2}", url, currentPageParamName,
                    number);
            }

            return newUrl;
        }
    }
}