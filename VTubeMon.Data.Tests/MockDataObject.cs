using System;
using System.Collections.Generic;
using System.Text;
using VTubeMon.API;

namespace VTubeMon.Data.Tests
{
    public class MockDataObject : IDataObject
    {
        public IList<IDataProperty> DataPropertyList { get; }

        public string ColumnNames { get; }

        public string Values { get; }

        public string InsertableColumnNames { get; }

        public string InsertableValues { get; }

        public string GetUpdateString(string asName)
        {
            return asName;
        }

        public void InitializeFromReader(INamedDataReader reader)
        {

        }
    }
}
