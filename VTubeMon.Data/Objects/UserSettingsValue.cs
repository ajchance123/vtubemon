using System.Collections.Generic;
using VTubeMon.Data;
using VTubeMon.Data.Objects;

namespace VTubeMon.API.Data.Objects
{
    public class UserSettingsValue : DataObjectBase, IUserSettingsValue
    {
        public UserSettingsValue()
        {

            IdUserSettingsValues = new DataProperty<int>("id_user_settings_values", (r) => r.GetInt32);
            IdUserSettingsMain = new DataProperty<int>("id_user_settings_main", (r) => r.GetInt32);
            IdUser = new DataProperty<ulong>("id_user", (r) => r.GetUInt64);
            IdGuild = new DataProperty<ulong>("id_guild", (r) => r.GetUInt64);
            Value = new StringDataProperty("value");

            DataPropertyList = new List<IDataProperty>()
            {
                IdUserSettingsValues,
                IdUserSettingsMain,
                IdUser,
                IdGuild,
                Value
            };
        }

        public UserSettingsValue(int idUserSettingsValues, int idUserSetting, ulong idUser, ulong idGuild, string value)
        {
            IdUserSettingsValues = new DataProperty<int>("id_user_settings_values", (r) => r.GetInt32, idUserSettingsValues);
            IdUserSettingsMain = new DataProperty<int>("id_user_settings_main", (r) => r.GetInt32, idUserSetting);
            IdUser = new DataProperty<ulong>("id_user", (r) => r.GetUInt64, idUser);
            IdGuild = new DataProperty<ulong>("id_guild", (r) => r.GetUInt64, idGuild);
            Value = new StringDataProperty("value", value);

            DataPropertyList = new List<IDataProperty>()
            {
                IdUserSettingsValues,
                IdUserSettingsMain,
                IdUser,
                IdGuild,
                Value
            };
        }

        public IDataProperty<int> IdUserSettingsValues { get; }
        public IDataProperty<int> IdUserSettingsMain { get; }
        public IDataProperty<ulong> IdUser { get; }
        public IDataProperty<ulong> IdGuild { get; }
        public IDataProperty<string> Value { get; }
        public override IList<IDataProperty> DataPropertyList { get; }
    }
}
