using System;
using VTubeMon.API;

namespace VTubeMon.Data
{

    public abstract class DataProperty : IDataProperty
    {
        public abstract string ColumnName { get; }
        public abstract string ValueString { get; }
        public abstract void SetValue(INamedDataReader dataReader);
    }

    public class DataProperty<T> : DataProperty
    {
        public DataProperty(string columnName, Func<INamedDataReader, Func<string, T>> dataReaderToValueFactory)
        {
            _dataReaderToValueFactory = dataReaderToValueFactory;
            ColumnName = columnName;
        }

        private Func<INamedDataReader, Func<string, T>> _dataReaderToValueFactory;

        public T Value { get; private set; }

        public override void SetValue(INamedDataReader dataReader)
        {
            Value = _dataReaderToValueFactory(dataReader)(ColumnName);
        }

        public override string ColumnName { get; }
        public override string ValueString => Value.ToString();
    }

    public class StringDataProperty : DataProperty<string>
    {
        public StringDataProperty(string columnName) : base (columnName, (r) => r.GetString)
        {

        }

        public override string ValueString => $"'{Value.ToString()}'";
    }
    public class DateTimeDataProperty : DataProperty<DateTime>
    {
        public DateTimeDataProperty(string columnName) : base(columnName, (r) => r.GetDateTime)
        {

        }

        public override string ValueString => $"'{Value.ToString("yyyy-MM-dd")}'";
    }
}
