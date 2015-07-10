using PugTrace.Storage;
using System;

namespace PugTrace
{
    public abstract class TraceStorage
    {
        private static readonly object LockObject = new object();
        private static TraceStorage _current;

        public static TraceStorage Current
        {
            get
            {
                lock (LockObject)
                {
                    if (_current == null)
                    {
                        throw new InvalidOperationException("TraceStorage.Current property value has not been initialized.");
                    }

                    return _current;
                }
            }
            set
            {
                lock (LockObject)
                {
                    _current = value;
                }
            }
        }

        public abstract IStorageConnection GetConnection();
    }
}