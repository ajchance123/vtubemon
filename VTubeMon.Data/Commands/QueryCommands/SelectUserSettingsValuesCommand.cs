using VTubeMon.API.Data.Objects;
using VTubeMon.Common;

namespace VTubeMon.Data.Commands.QueryCommands
{
    public class SelectUserSettingsValuesCommand : QueryCommand<UserSettingsValue>
    {
        public SelectUserSettingsValuesCommand() : base("user_settings_values")
        {

        }

        public SelectUserSettingsValuesCommand(IUser user) : base("user_settings_values", "*",
            new WhereStatement()
            {
                Equality = Equality.EqualTo,
                Value = $"{user.IdUser.Value}",
                Target = "id_user",
                UseQuotes = false
            },
            new WhereStatement()
            {
                Equality = Equality.EqualTo,
                Value = $"{user.IdGuild.Value}",
                Target = "id_guild",
                UseQuotes = false
            })
        {

        }
    }
}
