using System.Collections.Generic;
using System.Collections.ObjectModel;
using VTubeMon.API;
using VTubeMon.Data.Commands;
using VTubeMon.Data.Objects;
using VTubeMon.Wpf.Core.Components.Database.WorkItems.ActionParameters;
using VTubeMon.Wpf.Core.Components.Settings;

namespace VTubeMon.Wpf.Core.Components.Database.WorkItems.Actions.Custom
{
    public class SelectImageWorkItemAction : DatabaseWorkItemActionViewModelBase
    {
        public SelectImageWorkItemAction(IVTubeMonDbConnection vTubeMonDbConnection, IModelService modelService, IFileService fileService)
        {
            _vTuberSelectionActionParameter = new VTuberSelectionActionParameter(vTubeMonDbConnection.ExecuteDbQueryCommand(new SelectVTubersCommand()));
            _browseFileWorkItemActionParameter = new BrowseFileWorkItemActionParameter("Image Files |*.bmp;*.jpg;*.png");

            ActionParameters = new ObservableCollection<IDatabaseWorkItemActionParameter>()
            {
                _vTuberSelectionActionParameter,
                _browseFileWorkItemActionParameter
            };

            _modelService = modelService;
            _fileService = fileService;
        }

        private IModelService _modelService;
        private IFileService _fileService;
        private VTuberSelectionActionParameter _vTuberSelectionActionParameter;
        private BrowseFileWorkItemActionParameter _browseFileWorkItemActionParameter;
        public override string Name => "Select Image";

        public override ICollection<IDatabaseWorkItemActionParameter> ActionParameters { get; }

        protected override IDbNonQueryCommand CreateNonDbQueryCommand()
        {
            var imageFolder = _modelService.LoadModel<SettingsModel>().ImageFolder;
            var relativePath = _fileService.GetRelativePath(imageFolder, _browseFileWorkItemActionParameter.ParameterValue);
            relativePath = relativePath.Replace(@"\", @"\\");

            var insertCommand = new InsertCommand<VTuberImage>("vtubers_images", new VTuberImage(_vTuberSelectionActionParameter.VtuberId, relativePath));
            return insertCommand;
        }
    }
}
