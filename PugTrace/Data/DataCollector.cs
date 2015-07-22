using System.Collections.Generic;

namespace PugTrace.Data
{
    public sealed class DataCollector
    {
        private List<ITraceData> Data { get; set; }

        public DataCollector()
        {
            this.Data = new List<ITraceData>();
        }

        public void Add(ITraceData data)
        {
            this.Data.Add(data);
        }

        public ITraceData[] ToData()
        {
            return this.Data.ToArray();
        }
    }
}