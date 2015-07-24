using System;
using System.Collections.Generic;

namespace PugTrace.Storage
{
    public interface IStorageConnection : IDisposable
    {
        int Count();

        IEnumerable<TraceData> Get(int skip = 0, int top = 20);

        TraceData GetTraceDetail(int id);
    }
}
