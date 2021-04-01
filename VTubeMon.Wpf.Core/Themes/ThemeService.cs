using System;
using System.Collections.Generic;

namespace VTubeMon.Wpf.Core.Themes
{
    public class ThemeService
    {
        private ThemeResourceDictionary _themeResourceDictionary;
        public ThemeService(ThemeResourceDictionary resourceDictionary)
        {
            _themeResourceDictionary = resourceDictionary;
        }

        public IEnumerable<string> Themes => _themeResourceDictionary.Themes;
        public void SetTheme(string theme) => _themeResourceDictionary.SetTheme(theme);
        public string SelectedTheme => _themeResourceDictionary.SelectedTheme;
    }
}
