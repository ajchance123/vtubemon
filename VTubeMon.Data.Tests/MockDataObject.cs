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

        public void InitializeFromReader(INamedDataReader reader)
        {

        }
    }
}
