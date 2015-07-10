using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace PugTrace.Dashboard
{
    public abstract class RazorPage
    {
        private readonly StringBuilder _content = new StringBuilder();
        private string _body;

        protected RazorPage()
        {
            GenerationTime = Stopwatch.StartNew();
            Html = new HtmlHelper(this);
        }

        public RazorPage Layout { get; protected set; }
        public HtmlHelper Html { get; private set; }
        public UrlHelper Url { get; private set; }

        public TraceStorage Storage { get; private set; }
        public string AppPath { get; internal set; }
        public Stopwatch GenerationTime { get; private set; }

        internal IOwinRequest Request { private get; set; }
        internal IOwinResponse Response { private get; set; }

        public string RequestPath
        {
            get { return Request.Path.Value; }
        }

        public abstract void Execute();

        public string Query(string key)
        {
            return Request.Query[key];
        }

        public override string ToString()
        {
            return TransformText(null);
        }

        public void Assign(RazorPage parentPage)
        {
            Request = parentPage.Request;
            Response = parentPage.Response;
            AppPath = parentPage.AppPath;
            Storage = parentPage.Storage;
            Url = parentPage.Url;

            GenerationTime = parentPage.GenerationTime;
        }

        internal void Assign(RequestDispatcherContext context)
        {
            var owinContext = new OwinContext(context.OwinEnvironment);

            Request = owinContext.Request;
            Response = owinContext.Response;
            Storage = context.Storage;
            AppPath = context.AppPath;
            Url = new UrlHelper(context.OwinEnvironment);
        }

        protected void WriteLiteral(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
                return;
            _content.Append(textToAppend);
        }

        protected virtual void Write(object value)
        {
            if (value == null)
                return;
            var html = value as NonEscapedString;
            WriteLiteral(html != null ? html.ToString() : Encode(value.ToString()));
        }

        protected virtual object RenderBody()
        {
            return new NonEscapedString(_body);
        }

        private string TransformText(string body)
        {
            _body = body;

            Execute();

            if (Layout != null)
            {
                Layout.Assign(this);
                return Layout.TransformText(_content.ToString());
            }

            return _content.ToString();
        }

        private static string Encode(string text)
        {
            return string.IsNullOrEmpty(text)
                       ? string.Empty
                       : WebUtility.HtmlEncode(text);
        }
    }

}