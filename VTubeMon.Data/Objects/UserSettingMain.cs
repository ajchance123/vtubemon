using System.Collections.Generic;
using VTubeMon.API;
using VTubeMon.API.Data.Objects;

namespace VTubeMon.Data.Objects
{
    public class UserSettingMain : DataObjectBase, IUserSettingsMain
    {
        public UserSettingMain()
        {
            IdUserSettingsMain = new DataProperty<int>("id_user_settings_main", (r) => r.GetInt32);
            SettingName = new StringDataProperty("setting_name");

            DataPropertyList = new List<IDataProperty>()
            {
                IdUserSettingsMain,
                SettingName
            };
        }

        public UserSettingMain(int idUserSetting, string settingName)
        {
            IdUserSettingsMain = new DataProperty<int>("id_user_settings_main", (r) => r.GetInt32, idUserSetting);
            SettingName = new StringDataProperty("setting_name", settingName);

            DataPropertyList = new List<IDataProperty>()
            {
                IdUserSettingsMain,
                SettingName
            };
        }

        public IDataProperty<int> IdUserSettingsMain { get; }
        public IDataProperty<string> SettingName { get; }

        public override IList<IDataProperty> DataPropertyList { get; }
    }
}
