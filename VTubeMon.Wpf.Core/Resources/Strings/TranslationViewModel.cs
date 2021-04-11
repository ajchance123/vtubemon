using Prism.Mvvm;
using System.ComponentModel;

namespace VTubeMon.Wpf.Core.Resources.Strings
{
    [DefaultBindingProperty("Value")]
    public class TranslationViewModel : BindableBase
    {
        public TranslationViewModel(Translation translation)
        {
            Translation = translation;
        }

        private Translation _translation;
        public Translation Translation
        {
            get => _translation;
            set
            {
                _translation = value;
                RaisePropertyChanged(nameof(Value));
            }
        }

        public string Key => Translation.Key;
        public string Value => Translation.Value ?? Translation.Key;

        public override string ToString()
        {
            return Value;
        }
    }
}
