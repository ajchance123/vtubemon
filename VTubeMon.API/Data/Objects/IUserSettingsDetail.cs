
namespace VTubeMon.API.Data.Objects
{
    public interface IUserSettingsDetail
    {
        IDataProperty<int> IdUserSettingsOption { get; }
        IDataProperty<int> IdUserSettings { get; }
        IDataProperty<string> Detail { get; }
    }
}
