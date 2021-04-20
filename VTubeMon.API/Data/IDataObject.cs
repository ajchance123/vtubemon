

using System.Collections.Generic;

namespace VTubeMon.API
{
    public interface IDataObject
    {
        void InitializeFromReader(INamedDataReader reader);

        IList<IDataProperty> DataPropertyList { get; }

        string ColumnNames { get; }
        string InsertableColumnNames { get; }
        string GetUpdateString(string asName);
        string Values { get; }
        string InsertableValues { get; }
    }
}
