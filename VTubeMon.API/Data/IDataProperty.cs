namespace VTubeMon.API
{
    public interface IDataProperty
    {
        string ColumnName { get; }
        string ValueString { get; }
        void SetValue(INamedDataReader dataReader);
    }
}