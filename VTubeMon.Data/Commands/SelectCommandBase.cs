using System;
using System.Collections.Generic;
using System.Text;
using VTubeMon.API;

namespace VTubeMon.Data.Commands
{
    public abstract class SelectCommandBase<T> : IDbQueryCommand<T>
        where T : IDataObject, new()
    {
        protected abstract string Table { get; }
        protected virtual string Columns { get; } = "*";
        protected virtual IEnumerable<WhereStatement> WhereCollection { get; }
        public string Statement => $"SELECT {Columns} FROM {Table}{GetWhereCollection()}";

        private string GetWhereCollection()
        {
            if (WhereCollection == null) return string.Empty;

            var statement = string.Empty;

            bool first = true;
            foreach(var where in WhereCollection)
            {
                statement = statement + (first ? "WHERE" : "AND") + where.Statement;
                first = false;
            }

            return statement;
        }

        public IEnumerable<T> ReadData(INamedDataReader namedDataReader)
        {
            using (namedDataReader)
            {
                while (namedDataReader.Read())
                {
                    var dataObject = new T();
                    dataObject.InitializeFromReader(namedDataReader);
                    yield return dataObject;
                }
            }
        }
    }
}