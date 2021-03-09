using System;
using System.Collections.Generic;
using System.Text;
using VTubeMon.API;

namespace VTubeMon.Data.Commands
{
    public class StringSelectCommand : IDbSelectCommand<string>
    {
        public StringSelectCommand(string table, string column)
        {
            _table = table;
            _column = column;
        }

        private string _table;
        private string _column;
        public string Statement => $"SELECT {_column} FROM {_table}";

        public IEnumerable<string> ReadData(INamedDataReader namedDataReader)
        {
            using (namedDataReader)
            {
                yield return namedDataReader.GetString(_column);
            }
        }
    }
}
