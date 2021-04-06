namespace VTubeMon.Wpf.Core.Resources.Strings
{
    public class Translation
    {
        public Translation(string key, string translation)
        {
            Key = key;
            Value = translation;
        }
        public string Key { get; }
        public string Value { get; }

        public override string ToString()
        {
            return Value ?? Key;
        }
    }
}
