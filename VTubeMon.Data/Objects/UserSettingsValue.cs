using System.Collections.Generic;
using VTubeMon.Data;
using VTubeMon.Data.Objects;

namespace VTubeMon.API.Data.Objects
{
    public class UserSettingsValue : DataObjectBase, IUserSettingsValue
    {
        public UserSettingsValue()
        {
            IdUserSettingsMain = new DataProperty<int>("id_user_settings_main", (r) => r.GetInt32);
            IdUser = new DataProperty<ulong>("id_user", (r) => r.GetUInt64);
            IdGuild = new DataProperty<ulong>("id_guild", (r) => r.GetUInt64);
            Value = new StringDataProperty("value");

            DataPropertyList = new List<IDataProperty>()
            {
                IdUserSettingsMain,
                IdUser,
                IdGuild,
                Value
            };
        }

        public UserSettingsValue(/*int idUserSettingsValues,*/ int idUserSetting, ulong idUser, ulong idGuild, string value)
        {
            IdUserSettingsMain = new DataProperty<int>("id_user_settings_main", (r) => r.GetInt32, idUserSetting);
            IdUser = new DataProperty<ulong>("id_user", (r) => r.GetUInt64, idUser);
            IdGuild = new DataProperty<ulong>("id_guild", (r) => r.GetUInt64, idGuild);
            Value = new StringDataProperty("value", value);

            DataPropertyList = new List<IDataProperty>()
            {
                IdUserSettingsMain,
                IdUser,
                IdGuild,
                Value
            };
        }
        public IDataProperty<int> IdUserSettingsMain { get; }
        public IDataProperty<ulong> IdUser { get; }
        public IDataProperty<ulong> IdGuild { get; }
        public IDataProperty<string> Value { get; }
        public override IList<IDataProperty> DataPropertyList { get; }
    }
}
