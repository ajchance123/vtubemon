using System.Collections.Generic;
using VTubeMon.Wpf.Core.Resources.Strings;

namespace VTubeMon.Wpf.Core.Resources
{
    public class StringsService : ResourceService<StringsResourceDictionary>
    {
        public StringsService(StringsResourceDictionary resource) : base(resource)
        {
            base.OnResourceNameChanged += StringsService_OnResourceNameChanged;
        }

        private void StringsService_OnResourceNameChanged(object sender, string e)
        {
            foreach(var key in _autoTranslations.Keys)
            {
                _autoTranslations[key].Translation = Translate(key);
            }
        }

        public Translation Translate(string key)
        {
            var translated =  _resource[key] as string;

            return new Translation(key, translated);
        }

        private IDictionary<string, TranslationViewModel> _autoTranslations = new Dictionary<string, TranslationViewModel>();

        public TranslationViewModel AutoTranslate(string key)
        {
            var translation = Translate(key);
            var vm = new TranslationViewModel(translation);
            _autoTranslations.Add(key, vm);

            return vm;
        }
    }
}
