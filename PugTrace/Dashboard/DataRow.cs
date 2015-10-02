namespace PugTrace.Dashboard
{
    public class DataRow
    {
        public DataRow(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}