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

namespace VTubeMon.Wpf.Core.Components.Database.Users.Details
{
    public class UserSettingsDetailCollectionViewModel : BindableBase
    {
        public UserSettingsDetailCollectionViewModel(StringsService stringService, IVTubeMonDbConnection vTubeMonDbConnection)
        {
            _stringService = stringService;
            _vTubeMonDbConnection = vTubeMonDbConnection;

            var list = new List<IUserSettingsMain>(_vTubeMonDbConnection.ExecuteDbQueryCommand(new SelectUserSettingsMainCommand()));

            UserSettingsMainCollection = new ObservableCollection<UserSettingsMainViewModel>(list.Select(u => new UserSettingsMainViewModel(_vTubeMonDbConnection, u)));
        }

        private StringsService _stringService;
        private IVTubeMonDbConnection _vTubeMonDbConnection;

        public ICollection<UserSettingsMainViewModel> UserSettingsMainCollection { get; }

        public ICommand SaveCommand => new DelegateCommand(() =>
        {
            var currentCollection = UserSettingsMainCollection.Where(us => us.UserSettingMain?.IdUserSettingsMain?.Value != null);
            if (currentCollection.Any())
            {
                _vTubeMonDbConnection.ExecuteDbNonQueryCommand(
                    new InsertCommand<UserSettingMain>("user_settings_main",
                        currentCollection.Select(u =>
                            new UserSettingMain(
                                u.UserSettingMain.IdUserSettingsMain.Value,
                                u.SettingName)
                            ).ToArray())
                    { OnDuplicateKeyUpdate = true });
            }

            var nullCollection = UserSettingsMainCollection.Where(us => us.UserSettingMain?.IdUserSettingsMain?.Value == null);
            if (nullCollection.Any())
            {
                _vTubeMonDbConnection.ExecuteDbNonQueryCommand(
                    new InsertCommand<UserSettingMain>("user_settings_main",
                        nullCollection.Select(u =>
                            new UserSettingMain(
                                0,
                                u.SettingName)
                            ).ToArray()));
            }
        });

        public ICommand NewCommand => new DelegateCommand(() =>
        {
            UserSettingsMainCollection.Add(new UserSettingsMainViewModel(_vTubeMonDbConnection));
        });
    }

}
