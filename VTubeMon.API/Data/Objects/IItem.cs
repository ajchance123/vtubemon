using System;
using System.Collections.Generic;
using System.Text;

namespace VTubeMon.API.Data.Objects
{
    public interface IItem : IDataObject
    {
        IDataProperty<int> IdItem { get; }
        IDataProperty<string> ItemName { get; }
        IDataProperty<int> IdCategory { get; }
        IDataProperty<int> Price { get; }
    }
}
