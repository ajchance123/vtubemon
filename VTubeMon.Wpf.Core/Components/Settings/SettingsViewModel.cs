using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using VTubeMon.Wpf.Core.Resources;
using VTubeMon.Wpf.Core.Resources.Strings;

namespace VTubeMon.Wpf.Core.Components.Settings
{
    public class SettingsViewModel : BindableBase
    {
        public SettingsViewModel(ThemeService themeService, StringsService stringsService)
        {
            ThemeCollection = new ObservableCollection<Translation>(themeService.ResourceNames.Select(s => stringsService.Translate(s)));
            LanguageCollection = new ObservableCollection<string>(stringsService.ResourceNames);

            _themeService = themeService;
            _stringsService = stringsService;
            _stringsService.OnResourceNameChanged += _stringsService_OnResourceNameChanged;

            _selectedTheme = ThemeCollection.Single(t => t.Key == _themeService.SelectedResource);
            _selectedLanguage = stringsService.SelectedResource;
        }

        private void _stringsService_OnResourceNameChanged(object sender, string e)
        {
            string selectedTheme = SelectedTheme.Key;

            ThemeCollection.Clear();
            foreach(var theme in _themeService.ResourceNames.Select(s => _stringsService.Translate(s)))
            {
                ThemeCollection.Add(theme);
            }

            SelectedTheme = ThemeCollection.Single(t => t.Key == selectedTheme);
        }

        private ThemeService _themeService;
        public ICollection<Translation> ThemeCollection { get; }
        private Translation _selectedTheme;
        public Translation SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (value == null) return;

                SetProperty(ref _selectedTheme, value);
                _themeService.SetResource(value.Key);
            }
        }

        private StringsService _stringsService;
        public ICollection<string> LanguageCollection { get; }

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set 
            {
                SetProperty(ref _selectedLanguage, value);
                _stringsService.SetResource(value);
            }
        }
    }
}
