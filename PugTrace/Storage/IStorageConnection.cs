using System;
using System.Collections.Generic;

namespace PugTrace.Storage
{
    public interface IStorageConnection : IDisposable
    {
        int Count(string typeFilter = null);

        IEnumerable<TraceData> Get(int skip = 0, int top = 20, string typeFilter = null);

        TraceData GetTraceDetail(int id);
    }
}
