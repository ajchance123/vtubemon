using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VTubeMon.API;
using VTubeMon.Wpf.Core.Components.Database.WorkItems;
using VTubeMon.Wpf.Core.Components.Database.WorkItems.Custom;
using VTubeMon.Wpf.Core.Resources;

namespace VTubeMon.Wpf.Core.Components.Database
{
    public class DatabaseViewModel : BindableBase
    {
        public DatabaseViewModel(StringsService stringService, IModelService modelService, IFileService fileService, IVTubeMonDbConnection vTubeMonDbConnection, DatabaseWorkspace databaseWorkspace)
        {
            QueryCollection = new ObservableCollection<DatabaseWorkItemViewModelBase>()
            {
                new VTubersWorkItem(stringService, vTubeMonDbConnection),
                new DailiesWorkItem(stringService, vTubeMonDbConnection),
                new UsersWorkItem(stringService, vTubeMonDbConnection),
                new UserSettingsWorkItem(stringService, vTubeMonDbConnection),
                new UserSettingsValuesWorkItem(stringService, vTubeMonDbConnection),
                new AgenciesWorkItem(stringService, vTubeMonDbConnection),
                new VTuberImagesWorkItem(stringService, vTubeMonDbConnection, modelService, fileService)
            };
        }

        public ICollection<DatabaseWorkItemViewModelBase> QueryCollection { get; }

        private bool _isUserSelected;
        public bool IsUsersSelected
        {
            get => _isUserSelected;
            set => SetProperty(ref _isUserSelected, value);
        }

        private bool _isCustomQueriesSelected;
        public bool IsCustomQueriesSelected
        {
            get => _isCustomQueriesSelected;
            set => SetProperty(ref _isCustomQueriesSelected, value);
        }

        private bool _isUserSettingOptionsSelected;
        public bool IsUserSettingOptionsSelected
        {
            get => _isUserSettingOptionsSelected;
            set => SetProperty(ref _isUserSettingOptionsSelected, value);
        }
    }
}
