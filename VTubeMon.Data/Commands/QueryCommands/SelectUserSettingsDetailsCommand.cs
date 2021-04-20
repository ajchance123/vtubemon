using VTubeMon.API.Data.Objects;
using VTubeMon.Common;
using VTubeMon.Data.Objects;

namespace VTubeMon.Data.Commands.QueryCommands
{
    public class SelectUserSettingsDetailsCommand : QueryCommand<UserSettingsDetail>
    {
        public SelectUserSettingsDetailsCommand() : base("user_settings_details")
        {

        }
        public SelectUserSettingsDetailsCommand(IUserSettingsMain userSetting) : base("user_settings_details", "*",
            new WhereStatement()
            {
                Equality = Equality.EqualTo,
                Value = $"{userSetting.IdUserSettingsMain.Value}",
                Target = "id_user_settings_main",
                UseQuotes = false
            })
        {

        }
    }
}
