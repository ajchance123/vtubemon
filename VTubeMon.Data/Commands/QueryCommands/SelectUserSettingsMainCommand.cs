using VTubeMon.Data.Objects;

namespace VTubeMon.Data.Commands.QueryCommands
{
    public class SelectUserSettingsMainCommand : QueryCommand<UserSettingMain>
    {
        public SelectUserSettingsMainCommand() : base("user_settings_main")
        {

        }
    }
}
