using System;
using VTubeMon.API;

namespace VTubeMon.Data
{

    public abstract class DataProperty : IDataProperty
    {
        public abstract string ColumnName { get; }
        public abstract string ValueString { get; }
        public abstract void SetValue(INamedDataReader dataReader);
        public abstract bool CanInsert { get; }
    }

    public class DataProperty<T> : IDataProperty<T>
    {
        public DataProperty(string columnName, Func<INamedDataReader, Func<string, T>> dataReaderToValueFactory, T defaultValue, bool canInsert = true) : this(columnName, dataReaderToValueFactory, canInsert)
        {
            Value = defaultValue;
        }

        public DataProperty(string columnName, Func<INamedDataReader, Func<string, T>> dataReaderToValueFactory, bool canInsert = true)
        {
            _dataReaderToValueFactory = dataReaderToValueFactory;
            ColumnName = columnName;
            CanInsert = canInsert;
        }

        private Func<INamedDataReader, Func<string, T>> _dataReaderToValueFactory;

        public virtual bool CanInsert { get; }
        public T Value { get; private set; }

        public void SetValue(INamedDataReader dataReader)
        {
            Value = _dataReaderToValueFactory(dataReader)(ColumnName);
        }

        public string ColumnName { get; }
        public virtual string ValueString => Value.ToString();
    }

    public class StringDataProperty : DataProperty<string>
    {
        public StringDataProperty(string columnName) : base (columnName, (r) => r.GetString)
        {

        }
        public StringDataProperty(string columnName, string value) : base(columnName, (r) => r.GetString, value)
        {

        }

        public override string ValueString => $"'{Value.ToString()}'";
    }
    public class DateTimeDataProperty : DataProperty<DateTime>
    {
        public DateTimeDataProperty(string columnName) : base(columnName, (r) => r.GetDateTime)
        {

        }

        public DateTimeDataProperty(string columnName, DateTime value) : base(columnName, (r) => r.GetDateTime, value)
        {

        }

        public override string ValueString => $"'{Value.ToString("yyyy-MM-dd")}'";
    }
}
