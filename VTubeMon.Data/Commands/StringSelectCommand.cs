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
                while(namedDataReader.Read())
                {
                    yield return namedDataReader.GetString(_column);
                }
            }
        }
    }

    public class MultiStringSelectCommand : IDbSelectCommand<IList<string>>
    {
        public MultiStringSelectCommand(string statement)
        {
            Statement = statement;
        }
        public string Statement { get; }

        public IEnumerable<IList<string>> ReadData(INamedDataReader namedDataReader)
        {
            int columns = namedDataReader.FieldCount;

            using (namedDataReader)
            {
                while (namedDataReader.Read())
                {
                    IList<string> row = new List<string>();

                    for(int i = 0; i < columns; i++)
                    {
                        row.Add(namedDataReader.GetFieldValue<object>(i).ToString());
                    }
                    yield return row;
                }
            }
        }
    }
}
