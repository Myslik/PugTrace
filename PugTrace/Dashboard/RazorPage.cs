﻿using Microsoft.Owin;
using System;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace PugTrace.Dashboard
{
    public abstract class RazorPage<TModel> : RazorPage
    {
        public TModel Model { get; set; }
    }

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
            OnAssigned();
        }

        internal void Assign(RequestDispatcherContext context)
        {
            var owinContext = new OwinContext(context.OwinEnvironment);

            Request = owinContext.Request;
            Response = owinContext.Response;
            Storage = context.Storage;
            AppPath = context.AppPath;
            Url = new UrlHelper(context.OwinEnvironment);
            OnAssigned();
        }

        public virtual void OnAssigned() { }

        protected void WriteLiteral(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
                return;
            _content.Append(textToAppend);
        }

        public virtual void WriteAttribute(string attr,
                                   Tuple<string, int> token1,
                                   Tuple<string, int> token2,
                                   Tuple<Tuple<string, int>, Tuple<object, int>, bool> token3)
        {
            object value;
            if (token3 != null)
                value = token3.Item2.Item1;
            else
                value = string.Empty;

            var output = token1.Item1 + value.ToString() + token2.Item1;

            _content.Append(output);
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