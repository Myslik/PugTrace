using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace PugTrace.Data
{
    public class RequestData : ITraceData
    {
        public string Url { get; private set; }
        public Dictionary<string, string> QueryString { get; private set; }
        public Dictionary<string, string> ServerVariables { get; private set; }
        public Dictionary<string, string> FormData { get; private set; }
        public Dictionary<string, string> Cookies { get; private set; }

        public RequestData(HttpContextBase context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var request = context.Request;

            Url = request.RawUrl;
            QueryString = ToDictionary(request.QueryString);
            ServerVariables = ToDictionary(request.ServerVariables);
            FormData = ToDictionary(request.Form);
            Cookies = ToDictionary(request.Cookies);
        }

        public static Dictionary<string, string> ToDictionary(NameValueCollection collection)
        {
            if (collection == null || collection.Count == 0)
            {
                return null;
            }

            var dict = new Dictionary<string, string>();

            foreach (var key in collection.AllKeys)
            {
                try
                {
                    dict.Add(key, collection[key]);
                }
                catch (HttpRequestValidationException ex)
                {
                    dict.Add(key, ex.Message);
                }
            }
            return dict;
        }

        public static Dictionary<string, string> ToDictionary(HttpCookieCollection collection)
        {
            if (collection == null || collection.Count == 0)
            {
                return null;
            }

            var dict = new Dictionary<string, string>();

            foreach (var key in collection.AllKeys)
            {
                var name = collection[key].Name;
                var value = collection[key].Value;
                if (dict.ContainsKey(name))
                {
                    dict[name] = string.Format("{0}|{1}", dict[name], value);
                }
                else
                {
                    dict.Add(collection[key].Name, collection[key].Value);
                }
            }
            return dict;
        }
    }
}