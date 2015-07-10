using Newtonsoft.Json.Linq;

namespace PugTrace.Dashboard.Pages
{
    partial class DataObjectPage
    {
        public DataObjectPage(JObject data)
        {
            this.Data = data;
        }

        public JObject Data { get; private set; }
    }
}