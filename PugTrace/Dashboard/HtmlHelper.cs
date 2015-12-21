using Newtonsoft.Json.Linq;
using PugTrace.Dashboard.Pages;
using PugTrace.Storage;
using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

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

        public NonEscapedString RenderData(string data)
        {
            var builder = new StringBuilder();
            JArray json = null;

            if (!string.IsNullOrEmpty(data))
            {
                json = JArray.Parse(data);
            }

            if (json != null)
            {
                foreach (var property in json)
                {
                    if (property.Type == JTokenType.Object)
                    {
                        var obj = property.Value<JObject>();
                        var dataObject = new DataObject(obj);

                        builder.AppendLine(DataObject(dataObject).ToString());
                    }
                }
            }

            return Raw(builder.ToString());
        }

        public NonEscapedString DataObject(DataObject data)
        {
            return RenderPartial(new DataObjectPage { Model = data });
        }

        public NonEscapedString DataRow(DataRow row)
        {
            return RenderPartial(new DataRowPage { Model = row });
        }

        public NonEscapedString Filter(Pager pager)
        {
            return RenderPartial(new Filter { Model = pager });
        }

        public NonEscapedString Search(Pager pager)
        {
            return RenderPartial(new Search { Model = pager });
        }

        public NonEscapedString Paginator(Pager pager)
        {
            if (pager == null) throw new ArgumentNullException("pager");
            return RenderPartial(new Paginator { Model = pager });
        }

        public NonEscapedString PerPageSelector(Pager pager)
        {
            if (pager == null) throw new ArgumentNullException("pager");
            return RenderPartial(new PerPageSelector { Model = pager });
        }

        public NonEscapedString TraceRow(TraceData data)
        {
            if (data == null) throw new ArgumentNullException("data");
            return RenderPartial(new TraceRowPage { Model = data });
        }

        public NonEscapedString RenderPartial(RazorPage partialPage)
        {
            partialPage.Assign(_page);
            return new NonEscapedString(partialPage.ToString());
        }

        public string Truncate(string value, int maxLength = 50)
        {
            return value != null && value.Length > maxLength ? value.Substring(0, 49) + "…" : value;
        }

        public NonEscapedString RenderDateTime(DateTime d)
        {
            return Raw(string.Format("{0}", d.ToString("yyyy-MM-dd hh:mm")));
        }

        public NonEscapedString Raw(string value)
        {
            return new NonEscapedString(value);
        }

        public string HtmlEncode(string text)
        {
            return WebUtility.HtmlEncode(text);
        }

        private static Regex exceptionPattern = new Regex(@"^(.*):(.*)(\n\s+at\s(.*))+$", RegexOptions.Compiled);
        public bool IsExceptionStackTrace(string value)
        {
            return exceptionPattern.IsMatch(value);
        }

        public NonEscapedString RenderExceptionStackTrace(string value)
        {
            return Raw(string.Format("<pre style=\"border: none; padding: 0; margin: 0;\"><code class=\"csharp\">{0}</code></pre>", value));
        }

        public NonEscapedString RenderValue(string value)
        {
            if (IsExceptionStackTrace(value))
            {
                return RenderExceptionStackTrace(value);
            }
            else
            {
                return new NonEscapedString(HtmlEncode(value));
            }
        }

        public NonEscapedString Link(string text, string href, string classes = "", bool isActive = false, bool isDisabled = false)
        {
            classes = string.Format("{0} {1} {2}", classes, isActive ? "active" : "", isDisabled ? "disabled" : "").Trim();
            var classAttr = string.IsNullOrEmpty(classes) ? "" : string.Format("class=\"{0}\" ", classes);
            return Raw(string.Format("<a {2}href=\"{1}\">{0}</a>", text, href, classAttr));
        }
    }
}