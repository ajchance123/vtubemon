using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VTubeMon.Wpf.Core.Themes;

namespace VTubeMon.Wpf.Core.Components.Settings
{
    public class SettingsViewModel : BindableBase
    {
        public SettingsViewModel(ThemeService themeService)
        {
            ThemeCollection = new ObservableCollection<string>(themeService.Themes);

            _themeService = themeService;
            _selectedTheme = _themeService.SelectedTheme;
        }

        private ThemeService _themeService;
        public ICollection<string> ThemeCollection { get; }
        private string _selectedTheme;
        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                SetProperty(ref _selectedTheme, value);
                _themeService.SetTheme(value);
            }
        }
    }
}
