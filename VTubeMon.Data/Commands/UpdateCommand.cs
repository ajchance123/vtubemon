using System;
using System.Collections.Generic;
using System.Linq;
using VTubeMon.API;

namespace VTubeMon.Data.Commands
{
    public class UpdateCommand<T> : IDbNonQueryCommand<T>
        where T : IDataObject
    {
        public UpdateCommand(string table, IEnumerable<T> dataObjects)
        {
            Table = table;
            DataObjects = dataObjects;
        }

        public UpdateCommand(string table, params T[] dataObjects)
        {
            Table = table;
            DataObjects = dataObjects;
        }

        public IEnumerable<T> DataObjects { get; }

        public string Table { get; }

        public string Statement
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
