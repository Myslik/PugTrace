using System;
using System.Diagnostics;
using System.Globalization;

namespace PugTrace
{
    public abstract class TraceListenerBase : TraceListener
    {
        const string CategorySeparator = ": ";

        protected TraceListenerBase()
            : base()
        {
        }

        protected TraceListenerBase(string name)
            : base(name)
        {
        }

        public override void Close()
        {
            Flush();
            base.Close();
        }

        public override sealed void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if ((base.Filter == null) || base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                WriteTrace(eventCache, source, eventType, id, null, null, new object[] { data });
            }
        }

        public override sealed void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            if ((base.Filter == null) || base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
            {
                WriteTrace(eventCache, source, eventType, id, null, null, data);
            }
        }

        public override sealed void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            if ((base.Filter == null) || base.Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
            {
                WriteTrace(eventCache, source, eventType, id, message, null, null);
            }
        }

        public override sealed void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            if ((base.Filter == null) || base.Filter.ShouldTrace(eventCache, source, eventType, id, format, args, null, null))
            {
                if (args == null)
                {
                    WriteTrace(eventCache, source, eventType, id, format, null, null);
                }
                else
                {
                    var message = string.Format(CultureInfo.CurrentCulture, format, args);
                    WriteTrace(eventCache, source, eventType, id, message, null, null);
                }
            }
        }

        public override sealed void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
        {
            if ((base.Filter == null) || base.Filter.ShouldTrace(eventCache, source, TraceEventType.Transfer, id, message, null, null, null))
            {
                var traceMessage = string.Format(CultureInfo.CurrentCulture, "{0}, relatedActivityId={1}", message, relatedActivityId);
                WriteTrace(eventCache, source, TraceEventType.Transfer, id, traceMessage, relatedActivityId, null);
            }
        }

        public override sealed void Write(object o)
        {
            Write(null, null, o);
        }

        public override sealed void Write(object o, string category)
        {
            Write(category, null, o);
        }

        public override sealed void Write(string message)
        {
            Write(null, message, null);
        }

        public override sealed void Write(string message, string category)
        {
            Write(category, message, null);
        }

        public override sealed void WriteLine(object o)
        {
            WriteLine(null, null, o);
        }

        public override sealed void WriteLine(object o, string category)
        {
            WriteLine(category, null, o);
        }

        public override sealed void WriteLine(string message)
        {
            WriteLine(null, message, null);
        }

        public override sealed void WriteLine(string message, string category)
        {
            WriteLine(category, message, null);
        }

        protected virtual void Write(string category, string message, object data)
        {
            TraceWriteAsEvent(category, message, data);
        }

        protected virtual void WriteLine(string category, string message, object data)
        {
            TraceWriteAsEvent(category, message, data);
        }

        protected abstract void WriteTrace(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message, Guid? relatedActivityId, object[] data);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Essential.Diagnostics.TraceListenerBase.WriteTrace(System.Diagnostics.TraceEventCache,System.String,System.Diagnostics.TraceEventType,System.Int32,System.String,System.Nullable<System.Guid>,System.Object[])")]
        private void TraceWriteAsEvent(string category, string message, object data)
        {
            if ((base.Filter == null) || base.Filter.ShouldTrace(null, null, TraceEventType.Verbose, 0, message, new object[] { category }, data, null))
            {
                if (data == null)
                {
                    if (category != null)
                    {
                        var categoryMessage = category + CategorySeparator + message;
                        WriteTrace(null, null, TraceEventType.Verbose, 0, categoryMessage, null, null);
                    }
                    else
                    {
                        WriteTrace(null, null, TraceEventType.Verbose, 0, message, null, null);
                    }
                }
                else
                {
                    WriteTrace(null, null, TraceEventType.Verbose, 0, category, null, new object[] { data });
                }
            }
        }

    }
}
