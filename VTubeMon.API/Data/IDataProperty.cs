namespace VTubeMon.API
{
    public interface IDataProperty
    {
        string ColumnName { get; }
        string ValueString { get; }
        void SetValue(INamedDataReader dataReader);
    }

    public interface IDataProperty<T> : IDataProperty
    {
        T Value { get; }
    }
}