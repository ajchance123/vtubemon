using System;
using System.Collections.Generic;
using System.Text;
using VTubeMon.API;
using VTubeMon.Wpf.Core.Components.Database.WorkItems.Actions.Custom;
using VTubeMon.Wpf.Core.Resources;

namespace VTubeMon.Wpf.Core.Components.Database.WorkItems.Custom
{
    public class VTuberImagesWorkItem : DatabaseWorkItemViewModelBase
    {
        public VTuberImagesWorkItem(StringsService stringsService, IVTubeMonDbConnection vTubeMonDbConnection, IModelService modelService, IFileService filesService) : base(stringsService, vTubeMonDbConnection,
            new List<DatabaseWorkItemActionViewModelBase>()
            {
                new SelectImageWorkItemAction(vTubeMonDbConnection, modelService, filesService)
            })
        {

        }

        public override string Table => "vtubers_images";
    }
}
