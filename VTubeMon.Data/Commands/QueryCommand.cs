using System.Collections.Generic;
using VTubeMon.API;

namespace VTubeMon.Data.Commands
{
    public class QueryCommand<T> : IDbQueryCommand<T>
        where T : IDataObject, new()
    {
        public QueryCommand()
        {
            WhereCollection = new List<WhereStatement>();
        }

        public QueryCommand(string table, string columns = "*", params WhereStatement[] whereStatements)
        {
            Table = table;
            Columns = columns;
            if(whereStatements != null && whereStatements.Length > 0)
            {
                WhereCollection = whereStatements;
            }
        }

        protected virtual string Table { get; }
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