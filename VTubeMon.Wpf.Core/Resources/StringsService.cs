using VTubeMon.Wpf.Core.Resources.Strings;

namespace VTubeMon.Wpf.Core.Resources
{
    public class StringsService : ResourceService<StringsResourceDictionary>
    {
        public StringsService(StringsResourceDictionary resource) : base(resource)
        {
        }

        public Translation Translate(string key)
        {
            var translated =  _resource[key] as string;

            return new Translation(key, translated);
        }
    }
}
