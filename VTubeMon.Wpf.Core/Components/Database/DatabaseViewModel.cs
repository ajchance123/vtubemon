using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VTubeMon.API;
using VTubeMon.Data.Commands;
using VTubeMon.Data.Objects;
using VTubeMon.Wpf.Core.Components.Database.WorkItems.ActionParameters;
using VTubeMon.Wpf.Core.Components.Settings;
using VTubeMon.Wpf.Core.Resources;

namespace VTubeMon.Wpf.Core.Components.Database
{
    public class DatabaseViewModel : BindableBase
    {
        public DatabaseViewModel(StringsService stringService, IModelService modelService, IFileService fileService, IVTubeMonDbConnection vTubeMonDbConnection, DatabaseWorkspace databaseWorkspace)
        {
            _databaseWorkspace = databaseWorkspace;
            _modelService = modelService;
            _fileService = fileService;
            _vTubeMonDbConnection = vTubeMonDbConnection;

            QueryCollection = new ObservableCollection<DatabaseWorkItemViewModel>()
            {
                new DatabaseWorkItemViewModel(vTubeMonDbConnection)
                {
                    Name = "vtubers"
                },
                new DatabaseWorkItemViewModel(vTubeMonDbConnection)
                {
                    Name = "dailies"
                },
                new DatabaseWorkItemViewModel(vTubeMonDbConnection)
                {
                    Name = "users"
                },
                new DatabaseWorkItemViewModel(vTubeMonDbConnection)
                {
                    Name = "agencies"
                },
            };

            AddImagesWorkItem();
        }

        private void AddImagesWorkItem()
        {
            var vtuberSelectionActionParameter = new VTuberSelectionActionParameter(_vTubeMonDbConnection.ExecuteDbQueryCommand(new SelectVTubersCommand()));

            var browseImagepathActionParameter = new BrowseFileWorkItemActionParameter("Image Files |*.bmp;*.jpg;*.png");

            Func<IDbNonQueryCommand> dbNonQueryCommandFactory = new Func<IDbNonQueryCommand>(() =>
            {
                var imageFolder = _modelService.LoadModel<SettingsModel>().ImageFolder;
                var relativePath = _fileService.GetRelativePath(imageFolder, browseImagepathActionParameter.ParameterValue);
                relativePath = relativePath.Replace(@"\",@"\\");

                var insertCommand = new InsertCommand<VTuberImage>("vtubers_images", new VTuberImage(vtuberSelectionActionParameter.VtuberId, relativePath));
                return insertCommand;
            });
            var addImageAction = new DatabaseWorkItemAction(
                "Add Image",
                _vTubeMonDbConnection,
                dbNonQueryCommandFactory,
                vtuberSelectionActionParameter,
                browseImagepathActionParameter
            );

            var viewModel = new DatabaseWorkItemViewModel(
                     _vTubeMonDbConnection,
                     addImageAction
                 )
            {
                Name = "vtubers_images"
            };

            QueryCollection.Add(viewModel);
        }

        public string Name { get; set; }
        private DatabaseWorkspace _databaseWorkspace;
        private IVTubeMonDbConnection _vTubeMonDbConnection;
        private IModelService _modelService;
        private IFileService _fileService;

        public ICollection<DatabaseWorkItemViewModel> QueryCollection { get; }
    }
}
