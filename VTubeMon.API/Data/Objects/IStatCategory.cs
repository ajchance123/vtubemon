using System;
using System.Collections.Generic;
using System.Text;

namespace VTubeMon.API.Data.Objects
{
    public interface IStatCategory
    {
        IDataProperty<int> IdStat { get; }
        IDataProperty<string> StatName { get; }
    }
}
