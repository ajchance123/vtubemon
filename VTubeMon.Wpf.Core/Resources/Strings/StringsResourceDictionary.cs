using System.Collections.Generic;

namespace VTubeMon.Wpf.Core.Resources.Strings
{
    public class StringsResourceDictionary : WpfResource
    {
        protected override Dictionary<string, string> ResourceToXamlMap { get; } = new Dictionary<string, string>()
        {
            { "English", "strings.en-US.xaml"},
            { "日本語", "strings.jp.xaml"},
        };

        protected override string BaseDirectory => "Resources/Strings/";
    }
}
