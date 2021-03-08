

using System.Collections.Generic;

namespace VTubeMon.API
{
    public interface IDataObject
    {
        void InitializeFromReader(INamedDataReader reader);

        IList<IDataProperty> DataPropertyList { get; }

        string ColumnNames { get; }
        string Values { get; }
    }
}
