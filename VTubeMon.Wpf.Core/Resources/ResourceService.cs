using System;
using System.Collections.Generic;

namespace VTubeMon.Wpf.Core.Resources
{
    public class ResourceService<T> where T : IResource
    {
        public ResourceService(T resource)
        {
            _resource = resource;
        }

        protected T _resource;
        public void SetResource(string resourceName)
        {
            _resource.SetResource(resourceName);
            OnResourceNameChanged?.Invoke(this, resourceName);
        }
        public string SelectedResource => _resource.SelectedResource;
        public IEnumerable<string> ResourceNames => _resource.ResourceNames;
        public event EventHandler<string> OnResourceNameChanged;
    }
}
