using System;
using System.Collections.Generic;

namespace PugTrace.Storage
{
    public interface IStorageConnection : IDisposable
    {
        IEnumerable<TraceData> Get(int take = 20);

        TraceData Get(string id);
    }
}
