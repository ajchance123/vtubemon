using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using VTubeMon.Wpf.Core.Resources;

namespace VTubeMon.Wpf.Core.Themes
{
    public class ThemeResourceDictionary : WpfResource
    {
        protected override Dictionary<string, string> ResourceToXamlMap { get; } = new Dictionary<string, string>()
        {
            { "Light", "LightMode.xaml"},
            { "Dark", "DarkMode.xaml"},
            { "White", "WhiteMode.xaml" },
            { "Contrast", "ContrastMode.xaml" },
        };

        protected override string BaseDirectory => "Resources/Themes/";

    }
}
