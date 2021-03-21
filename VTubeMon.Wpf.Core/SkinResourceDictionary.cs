using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace VTubeMon.Wpf.Core
{
    public class SkinResourceDictionary : ResourceDictionary
    {
        private Uri _DarkSource;
        private Uri _LightSource;
        private Uri _ContrastSource;

        public Uri DarkSource
        {
            get { return _DarkSource;  }
            set
            {
                _DarkSource = value;
                UpdateSource(Skin.Dark);
            }
        }

        public Uri LightSource
        {
            get { return _LightSource; }
            set
            {
                _LightSource = value;
                UpdateSource(Skin.Light);
            }
        }

        public Uri ContrastSource
        {
            get { return _ContrastSource; }
            set
            {
                _ContrastSource = value;
                UpdateSource(Skin.Contrast);
            }
        }

        public void UpdateSource(Skin newSkin)
        {
            if(newSkin == Skin.Light)
            {
                base.Source = LightSource;
            } else if (newSkin == Skin.Dark)
            {
                base.Source = DarkSource;
            } else
            {
                base.Source = ContrastSource;
            }
        }
    }
}
