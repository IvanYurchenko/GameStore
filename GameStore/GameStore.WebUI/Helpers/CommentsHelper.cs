using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Helpers
{
    public static class CommentsHelper
    {
        /// <summary>
        /// Gets the body of the comment with decoded tags.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="body">The body.</param>
        /// <returns></returns>
        public static MvcHtmlString GetCommentBodyWithDecodedTags(this HtmlHelper htmlHelper, string body)
        {
            string result = body
                .Replace("[quote]", "<blockquote>").Replace("[/quote]", "</blockquote>")
                .Replace("[author]", "<footer>").Replace("[/author]", "</footer>");
            return new MvcHtmlString(result);
        }
    }
}