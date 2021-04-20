
namespace VTubeMon.API.Data.Objects
{
    public interface IUser : IDataObject
    {
        IDataProperty<ulong> IdUser { get; }
        IDataProperty<ulong> IdGuild { get; }
        IDataProperty<bool> Admin { get; }
        IDataProperty<int> VTuberCash { get; }
    }
}
