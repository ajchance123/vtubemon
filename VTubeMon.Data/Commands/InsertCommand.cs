using System.Collections.Generic;
using System.Linq;
using VTubeMon.API;

namespace VTubeMon.Data.Commands
{
    public class InsertCommand<T> : IDbNonQueryCommand<T>
        where T : IDataObject
    {
        public InsertCommand(string table, IEnumerable<T> dataObjects)
        {
            Table = table;
            DataObjects = dataObjects;
        }

        public InsertCommand(string table, params T[] dataObjects)
        {
            Table = table;
            DataObjects = dataObjects;
        }

        public IEnumerable<T> DataObjects { get; }

        public string Table { get; }

        public string Statement => $"INSERT INTO {Table}{DataObjects.First().ColumnNames} VALUES {string.Join(",", DataObjects.Select(d => d.Values))}";
    }
}
