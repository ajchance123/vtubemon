using System.Collections.Generic;
using System.Linq;
using VTubeMon.API;

namespace VTubeMon.Data.Objects
{
    public abstract class DataObjectBase : IDataObject
    {

        public void InitializeFromReader(INamedDataReader reader)
        {
            foreach(var property in DataPropertyList)
            {
                property.SetValue(reader);
            }
        }

        public abstract IList<IDataProperty> DataPropertyList { get; }

        public string ColumnNames => $"({string.Join(",", DataPropertyList.Select(d => d.ColumnName))})";

        public string Values => $"({string.Join(",", DataPropertyList.Select(d => d.ValueString))})";
    }
}
