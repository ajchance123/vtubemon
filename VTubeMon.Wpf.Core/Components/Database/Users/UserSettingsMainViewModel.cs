using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using VTubeMon.API;
using VTubeMon.API.Data.Objects;
using VTubeMon.Data.Commands.QueryCommands;

namespace VTubeMon.Wpf.Core.Components.Database.Users
{
    public class UserSettingsMainViewModel : BindableBase
    {
        public UserSettingsMainViewModel(IVTubeMonDbConnection vtubeMonDbConnection, IUserSettingsMain userSetting = null)
        {
            UserSettingMain = userSetting;
            _vTubeMonDbConnection = vtubeMonDbConnection;
            if (UserSettingMain != null)
            {
                SettingName = UserSettingMain.SettingName.Value;
                UserSettingsDetailCollection = new ObservableCollection<UserSettingsDetailViewModel>(_vTubeMonDbConnection.ExecuteDbQueryCommand(new SelectUserSettingsDetailsCommand(userSetting)).Select(u => new UserSettingsDetailViewModel(u)));
            }

        }

        public IUserSettingsMain UserSettingMain { get; }
        private IVTubeMonDbConnection _vTubeMonDbConnection;

        public ICollection<UserSettingsDetailViewModel> UserSettingsDetailCollection { get; }
        public bool HasDetails => UserSettingsDetailCollection.Any();

        private UserSettingsDetailViewModel _selectedUserSettingsDetail;
        public UserSettingsDetailViewModel SelectedUserSettingsDetail
        {
            get => _selectedUserSettingsDetail;
            set
            {
                SetProperty(ref _selectedUserSettingsDetail, value);
                SettingValue = value?.Detail;
            }
        }

        private string _settingValue;
        public string SettingValue
        {
            get => _settingValue;
            set => SetProperty(ref _settingValue, value);
        }

        private string _settingName;
        public string SettingName
        {
            get => _settingName;
            set => SetProperty(ref _settingName, value);
        }

        private IUserSettingsValue _userSettingsValue;
        public IUserSettingsValue UserSettingsValue
        {
            get => _userSettingsValue;
            set
            {
                SetProperty(ref _userSettingsValue, value);
                if (!string.IsNullOrEmpty(value?.Value?.Value) && UserSettingsDetailCollection.Any())
                {
                    SelectedUserSettingsDetail = UserSettingsDetailCollection.Single(o => o.Detail == value.Value.Value);
                }
                SettingValue = value?.Value?.Value;
                // ...neat...thats not confusing at all
            }
        }

        public ICommand AddCommand => new DelegateCommand(() =>
        {
            UserSettingsDetailCollection.Add(new UserSettingsDetailViewModel());
        });
    }
}
