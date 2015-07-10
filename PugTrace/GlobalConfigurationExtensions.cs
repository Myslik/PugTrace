using System;
using System.ComponentModel;

namespace PugTrace
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class GlobalConfigurationExtensions
    {
        

        public static IGlobalConfiguration<TStorage> UseStorage<TStorage>(
            this IGlobalConfiguration configuration,
            TStorage storage)
            where TStorage : TraceStorage
        {
            if (configuration == null) throw new ArgumentNullException("configuration");
            if (storage == null) throw new ArgumentNullException("storage");

            return configuration.Use(storage, x => TraceStorage.Current = x);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IGlobalConfiguration<T> Use<T>(
            this IGlobalConfiguration configuration, T entry,
            Action<T> entryAction)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            entryAction(entry);

            return new ConfigurationEntry<T>(entry);
        }

        private class ConfigurationEntry<T> : IGlobalConfiguration<T>
        {
            private readonly T _entry;

            public ConfigurationEntry(T entry)
            {
                _entry = entry;
            }

            public T Entry
            {
                get { return _entry; }
            }
        }
    }
}