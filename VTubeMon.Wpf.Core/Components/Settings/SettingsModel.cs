using System;
using System.Collections.Generic;
using System.Text;

namespace VTubeMon.Wpf.Core.Components.Settings
{
    public class SettingsModel
    {
        public SettingsModel()
        {
            ImageFolder = @"C:\VTubeMonData\Images\";
        }
        public string SelectedTheme { get; set; }
        public string SelectedLanguage { get; set; }
        public string ImageFolder { get; set; }
    }
}
