using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using VTubeMon.API;
using VTubeMon.API.Data.Objects;
using VTubeMon.Data.Commands;
using VTubeMon.Data.Commands.QueryCommands;
using VTubeMon.Data.Objects;
using VTubeMon.Wpf.Core.Resources;

namespace VTubeMon.Wpf.Core.Components.Database.Users.Values
{
    public class UserViewModel : BindableBase
    {
        public UserViewModel(StringsService stringService, IVTubeMonDbConnection vTubeMonDbConnection)
        {
            _stringService = stringService;
            _vTubeMonDbConnection = vTubeMonDbConnection;
            UserCollection = new ObservableCollection<IUser>(vTubeMonDbConnection.ExecuteDbQueryCommand(new SelectUsersCommand()));
            UserSettingCollection = new ObservableCollection<UserSettingsMainViewModel>();

            InitializeUserSettings();
        }

        private StringsService _stringService;
        private IVTubeMonDbConnection _vTubeMonDbConnection;

        private IUser _selectedUser;
        public IUser SelectedUser
        {
            get => _selectedUser;
            set
            {
                SetProperty(ref _selectedUser, value);
                UpdateUserSettings();
            }
        }

        private void InitializeUserSettings()
        {
            IList<UserSettingMain> _userSettings = new List<UserSettingMain>(_vTubeMonDbConnection.ExecuteDbQueryCommand(new SelectUserSettingsMainCommand()));
            foreach (var setting in _userSettings)
            {
                UserSettingCollection.Add(new UserSettingsMainViewModel(_vTubeMonDbConnection, setting));
            }
        }
        private void UpdateUserSettings()
        {
            _selectedUserSettingValues = new List<UserSettingsValue>(_vTubeMonDbConnection.ExecuteDbQueryCommand(new SelectUserSettingsValuesCommand(SelectedUser)));

            foreach (var setting in UserSettingCollection)
            {
                setting.UserSettingsValue = _selectedUserSettingValues.SingleOrDefault(u => u.IdUserSettingsMain.Value == setting.UserSettingMain.IdUserSettingsMain.Value);
            }
        }

        private IList<UserSettingsValue> _selectedUserSettingValues;
        public ICollection<IUser> UserCollection { get; }
        public ICollection<UserSettingsMainViewModel> UserSettingCollection { get; }

        public ICommand SaveCommand => new DelegateCommand(() =>
        {
            var currentCollection = UserSettingCollection.Where(us => us.UserSettingsValue?.IdUserSettingsValues?.Value != null);
            if (currentCollection.Any())
            {
                _vTubeMonDbConnection.ExecuteDbNonQueryCommand(
                    new InsertCommand<UserSettingsValue>("user_settings_values",
                        currentCollection.Select(u =>
                            new UserSettingsValue(
                                u.UserSettingsValue.IdUserSettingsValues.Value,
                                u.UserSettingMain.IdUserSettingsMain.Value,
                                SelectedUser.IdUser.Value,
                                SelectedUser.IdGuild.Value,
                                u.SettingValue)
                            ).ToArray())
                    { OnDuplicateKeyUpdate = true });
            }

            var nullCollection = UserSettingCollection.Where(us => us.UserSettingsValue?.IdUserSettingsValues?.Value == null);
            if (nullCollection.Any())
            {
                _vTubeMonDbConnection.ExecuteDbNonQueryCommand(
                    new InsertCommand<UserSettingsValue>("user_settings_values",
                        nullCollection.Select(u =>
                            new UserSettingsValue(
                                0,
                                u.UserSettingMain.IdUserSettingsMain.Value,
                                SelectedUser.IdUser.Value,
                                SelectedUser.IdGuild.Value,
                                u.SettingValue)
                            ).ToArray()));
            }
        });
    }
}
