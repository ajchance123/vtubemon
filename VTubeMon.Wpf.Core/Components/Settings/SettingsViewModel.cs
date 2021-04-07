using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using VTubeMon.API;
using VTubeMon.Wpf.Core.IO;
using VTubeMon.Wpf.Core.Resources;
using VTubeMon.Wpf.Core.Resources.Strings;

namespace VTubeMon.Wpf.Core.Components.Settings
{
    public class SettingsViewModel : BindableBase
    {
        public SettingsViewModel(ThemeService themeService, StringsService stringsService, IModelService modelService)
        {
            ThemeCollection = new ObservableCollection<Translation>(themeService.ResourceNames.Select(s => stringsService.Translate(s)));
            LanguageCollection = new ObservableCollection<string>(stringsService.ResourceNames);

            _modelService = modelService;
            _themeService = themeService;
            _stringsService = stringsService;
            _stringsService.OnResourceNameChanged += _stringsService_OnResourceNameChanged;

            _selectedTheme = ThemeCollection.Single(t => t.Key == _themeService.SelectedResource);
            _selectedLanguage = stringsService.SelectedResource;

            ReadModel();
        }

        private void ReadModel()
        {
            var settingsModel = _modelService.LoadModel<SettingsModel>(_settingsFileName);
            _model = settingsModel ?? new SettingsModel();
            if (settingsModel != null)
            {
                _themeService.SetResource(settingsModel.SelectedTheme);
                _selectedTheme = ThemeCollection.Single(t => t.Key == settingsModel.SelectedTheme);
                RaisePropertyChanged(nameof(SelectedTheme));

                _stringsService.SetResource(settingsModel.SelectedLanguage);
                _selectedLanguage = settingsModel.SelectedLanguage;
                RaisePropertyChanged(nameof(SelectedLanguage));

                _localImageFolder = settingsModel.ImageFolder;
                RaisePropertyChanged(nameof(LocalImageFolder));
            }
        }

        private void SaveModel()
        {
            _modelService.SaveModel(_settingsFileName, _model);
        }

        private string _settingsFileName = "UserSettings.json";
        private IModelService _modelService;
        private SettingsModel _model;

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
                _model.SelectedTheme = value.Key;
                SaveModel();
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
                _model.SelectedLanguage = value;
                SaveModel();
            }
        }

        public ICommand BrowseLocalImageFolderCommand => new DelegateCommand(() =>
        {
            Ookii.Dialogs.Wpf.VistaFolderBrowserDialog vistaFolderBrowserDialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            var result = vistaFolderBrowserDialog.ShowDialog();

            if(result.HasValue && result.Value)
            {
                LocalImageFolder = vistaFolderBrowserDialog.SelectedPath;
            }
        });

        private string _localImageFolder;
        public string LocalImageFolder
        {
            get => _localImageFolder;
            set
            {
                SetProperty(ref _localImageFolder, value);
                _model.ImageFolder = value;
                SaveModel();
            }
        }
    }
}
