﻿using Newtonsoft.Json.Linq;
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

        public NonEscapedString Filter(Pager pager)
        {
            return RenderPartial(new Filter(pager));
        }

        public NonEscapedString Paginator(Pager pager)
        {
            if (pager == null) throw new ArgumentNullException("pager");
            return RenderPartial(new Paginator(pager));
        }

        public NonEscapedString PerPageSelector(Pager pager)
        {
            if (pager == null) throw new ArgumentNullException("pager");
            return RenderPartial(new PerPageSelector(pager));
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
            return Raw(string.Format("<abbr title=\"{0}\">{1}</abbr>", d.ToString("f"), d.GetPrettyDate()));
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