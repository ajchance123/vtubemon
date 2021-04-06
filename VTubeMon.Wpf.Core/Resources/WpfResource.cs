using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace VTubeMon.Wpf.Core.Resources
{
    public abstract class WpfResource : ResourceDictionary, IResource
    {
        public WpfResource()
        {
            SetResource(ResourceNames.First());
        }

        protected abstract Dictionary<string, string> ResourceToXamlMap { get; }

        protected abstract string BaseDirectory { get; }

        public IEnumerable<string> ResourceNames => ResourceToXamlMap.Keys;

        public string SelectedResource { get; private set; }

        public void SetResource(string resourceName)
        {
            if (!ResourceToXamlMap.ContainsKey(resourceName))
            {
                throw new ArgumentOutOfRangeException(resourceName);
            }

            SelectedResource = resourceName;

            base.Source = new Uri($"pack://application:,,,/{BaseDirectory}{ResourceToXamlMap[resourceName]}");
        }
    }
}
