using Newtonsoft.Json.Linq;
using PugTrace.Dashboard.Pages;
using System;
using System.Net;

namespace PugTrace.Dashboard
{
    public class HtmlHelper
    {
        private readonly RazorPage _page;

        public HtmlHelper(RazorPage page)
        {
            if (page == null) throw new ArgumentNullException("page");
            _page = page;
        }

        public NonEscapedString DataObject(JObject data)
        {
            return RenderPartial(new DataObjectPage(data));
        }

        public NonEscapedString RenderPartial(RazorPage partialPage)
        {
            partialPage.Assign(_page);
            return new NonEscapedString(partialPage.ToString());
        }

        public NonEscapedString Raw(string value)
        {
            return new NonEscapedString(value);
        }

        public string HtmlEncode(string text)
        {
            return WebUtility.HtmlEncode(text);
        }
    }
}