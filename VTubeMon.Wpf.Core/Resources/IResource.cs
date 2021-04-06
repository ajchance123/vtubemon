using System;
using System.Collections.Generic;
using System.Text;

namespace VTubeMon.Wpf.Core.Resources
{
    public interface IResource
    {
        void SetResource(string resourceName);
        IEnumerable<string> ResourceNames { get; }
        string SelectedResource { get; }
    }
}
