using System;
using System.Collections.Generic;
using System.Text;

namespace VTubeMon.API.Data.Objects
{
    public interface IItemCategory
    {
        IDataProperty<int> IdCategory { get; }
        IDataProperty<string> CategoryName { get; }
    }
}
