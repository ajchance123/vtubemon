
namespace VTubeMon.API.Data.Objects
{
    public interface IUserSettingsValue
    {
        IDataProperty<int> IdUserSettingsValues { get; }
        IDataProperty<int> IdUserSettingsMain { get; }
        IDataProperty<ulong> IdUser { get; }
        IDataProperty<ulong> IdGuild { get; }
        IDataProperty<string> Value { get; }
    }
}
