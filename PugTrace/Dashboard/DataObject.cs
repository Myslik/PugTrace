using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace PugTrace.Dashboard
{
    public class DataObject
    {
        public string Key { get; set; }

        public DataObject(JObject jObject, string key = "")
        {
            Key = key;
            Rows = new List<DataRow>();
            Objects = new List<DataObject>();
            foreach (var token in jObject)
            {
                if (token.Value.Type == JTokenType.Object)
                {
                    Objects.Add(new DataObject(token.Value.Value<JObject>(), token.Key));
                }
                else
                {
                    Rows.Add(new DataRow(token.Key, token.Value.ToString()));
                }
            }
        }

        public List<DataRow> Rows { get; set; }

        public List<DataObject> Objects { get; set; }
    }
}