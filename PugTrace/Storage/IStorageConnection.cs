using System;
using System.Collections.Generic;

namespace PugTrace.Storage
{
    public interface IStorageConnection : IDisposable
    {
        int Count(string typeFilter = null);

        IEnumerable<Trace> Get(int skip = 0, int top = 20, string typeFilter = null);

        Trace GetTraceDetail(int id);

        IEnumerable<Trace> Search(DateTime from, DateTime to, string searchValue = null, string typeFilter = null);

        void Cleanup();
    }
}
