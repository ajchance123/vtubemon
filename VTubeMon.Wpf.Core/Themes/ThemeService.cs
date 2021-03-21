using System;
using System.Collections.Generic;
using System.Text;

namespace VTubeMon.Wpf.Core.Themes
{
    public class ThemeService
    {
        public IList<Skin> skins = new List<Skin>() { Skin.Light, Skin.Dark, Skin.Contrast };

        public event EventHandler<Skin> onSkinsChanged;
        public void ChangeSkin(Skin newSkin)
        {
            onSkinsChanged?.Invoke(this, newSkin);
        }
    }
}
