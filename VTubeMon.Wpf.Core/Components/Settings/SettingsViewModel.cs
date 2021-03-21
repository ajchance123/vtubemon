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
            SkinCollection = new ObservableCollection<Skin>(themeService.skins);

            _themeService = themeService;
        }

        private ThemeService _themeService;
        public ICollection<Skin> SkinCollection { get; }
        private Skin _selectedSkin;
        public Skin SelectedSkin
        {
            get => _selectedSkin;
            set
            {
                SetProperty(ref _selectedSkin, value);
                _themeService.ChangeSkin(value);
            }
        }
    }
}
