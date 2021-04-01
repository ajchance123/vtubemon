using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace VTubeMon.Wpf.Core.Themes
{
    public class ThemeResourceDictionary : ResourceDictionary
    {
        public ThemeResourceDictionary()
        {
            SetTheme(Themes.First());
        }

        private Dictionary<string, string> _themeSources = new Dictionary<string, string>()
        {
            { "Light", "LightMode.xaml"},
            { "Dark", "DarkMode.xaml"},
            { "Contrast", "ContrastMode.xaml" },
        };

        public IEnumerable<string> Themes => _themeSources.Keys;

        public string SelectedTheme { get; private set; }
        public void SetTheme(string theme)
        {
            if (!_themeSources.ContainsKey(theme))
            {
                throw new ArgumentOutOfRangeException(theme);
            }

            SelectedTheme = theme;
            base.Source = new Uri($"pack://application:,,,/Themes/{_themeSources[theme]}");
        }
    }
}
