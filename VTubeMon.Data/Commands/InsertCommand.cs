using System;
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

        public bool Ignore { get; set; }
        public bool OnDuplicateKeyUpdate { get; set; }
        public IEnumerable<T> DataObjects { get; }

        public string Table { get; }

        public string Statement
        {
            get
            {
                string columnNames = OnDuplicateKeyUpdate ? DataObjects.First().ColumnNames : DataObjects.First().InsertableColumnNames;
                var statement = $"INSERT {(Ignore ? "IGNORE " : string.Empty)}INTO {Table}{columnNames} VALUES {string.Join(",", DataObjects.Select(d => OnDuplicateKeyUpdate ? d.Values : d.InsertableValues))}";

                if (OnDuplicateKeyUpdate)
                {
                    statement += $"{Environment.NewLine}As new{Environment.NewLine}ON DUPLICATE KEY UPDATE{Environment.NewLine}{DataObjects.First().GetUpdateString("new")}";
                }
                return statement;
            }
        }
    }
}
