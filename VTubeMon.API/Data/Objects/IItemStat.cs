using System;
using System.Collections.Generic;
using System.Text;

namespace VTubeMon.API.Data.Objects
{
    public interface IItemStat
    {
        IDataProperty<int> IdItem { get; }
        IDataProperty<int> IdStat { get; }
        IDataProperty<int> StatValue { get; }
    }
}
