using System.Collections.Generic;
using VTubeMon.API;
using VTubeMon.API.Data.Objects;

namespace VTubeMon.Data.Objects
{
    public class UserSettingsDetail : DataObjectBase, IUserSettingsDetail
    {
        public UserSettingsDetail()
        {
            IdUserSettingsOption = new DataProperty<int>("id_user_settings_details", (r) => r.GetInt32);
            IdUserSettings = new DataProperty<int>("id_user_settings_main", (r) => r.GetInt32);
            Detail = new StringDataProperty("detail");

            DataPropertyList = new List<IDataProperty>()
            {
                IdUserSettingsOption,
                IdUserSettings,
                Detail
            };
        }

        public UserSettingsDetail(int idUserSettingsOption, int idUserSetting, string settingName)
        {
            IdUserSettingsOption = new DataProperty<int>("id_user_settings_details", (r) => r.GetInt32, idUserSettingsOption);
            IdUserSettings = new DataProperty<int>("id_user_settings_main", (r) => r.GetInt32, idUserSetting);
            Detail = new StringDataProperty("detail", settingName);

            DataPropertyList = new List<IDataProperty>()
            {
                IdUserSettingsOption,
                IdUserSettings,
                Detail
            };
        }

        public IDataProperty<int> IdUserSettingsOption { get; }
        public IDataProperty<int> IdUserSettings { get; }
        public IDataProperty<string> Detail { get; }
        public override IList<IDataProperty> DataPropertyList { get; }
    }
}
