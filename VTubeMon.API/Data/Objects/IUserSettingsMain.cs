
namespace VTubeMon.API.Data.Objects
{
    public interface IUserSettingsMain : IDataObject
    {
        IDataProperty<int> IdUserSettingsMain { get; }
        IDataProperty<string> SettingName { get; }
    }
}
