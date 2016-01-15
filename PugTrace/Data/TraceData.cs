using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PugTrace.Data
{
    public class TraceData
    {
        public TraceData()
        {
            Parameters = new Dictionary<string, object>();
        }

        public TraceData(string format, object parameters) : this()
        {
            Format = format;
            Parameters = GetPropertyDictionary(parameters);
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Format) && Parameters != null;
        }

        public string Format { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public string StackTrace { get; set; }

        public override string ToString()
        {
            string message = Format;
            if (message == null || Parameters == null)
                return message;
            
            foreach(var parameter in Parameters)
            {
                message = InjectSingleValue(message, parameter.Key, parameter.Value);
            }
            return message;
        }

        public string ToHTML()
        {
            string message = Format;
            if (message == null || Parameters == null)
                return message;

            foreach (var parameter in Parameters)
            {
                var html = string.Format("<span class=\"parameter\">{0}</span>", parameter.Value);
                message = InjectSingleValue(message, parameter.Key, html);
            }
            return message;
        }

        private static Dictionary<string, object> GetPropertyDictionary(object properties)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            if (properties != null)
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(properties);
                foreach (PropertyDescriptor prop in props)
                {
                    values.Add(prop.Name, prop.GetValue(properties));
                }
            }
            return values;
        }

        private static string InjectSingleValue(string formatString, string key, object replacementValue)
        {
            string result = formatString;
            Regex attributeRegex = new Regex("{(" + key + ")(?:}|(?::(.[^}]*)}))");

            foreach (Match m in attributeRegex.Matches(formatString))
            {
                string replacement = m.ToString();
                if (m.Groups[2].Length > 0)
                {
                    string attributeFormatString = string.Format(CultureInfo.InvariantCulture, "{{0:{0}}}", m.Groups[2]);
                    replacement = string.Format(CultureInfo.CurrentCulture, attributeFormatString, replacementValue);
                }
                else
                {
                    replacement = (replacementValue ?? string.Empty).ToString();
                }
                result = result.Replace(m.ToString(), replacement);
            }
            return result;
        }
    }
}