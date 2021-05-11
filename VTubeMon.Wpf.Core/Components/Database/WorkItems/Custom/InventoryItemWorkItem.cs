using System.Collections.Generic;
using VTubeMon.API;
using VTubeMon.Wpf.Core.Resources;

namespace VTubeMon.Wpf.Core.Components.Database.WorkItems.Custom
{
    public class InventoryItemWorkItem : DatabaseWorkItemViewModelBase
    {
        public InventoryItemWorkItem(StringsService stringsService, IVTubeMonDbConnection vTubeMonDbConnection) : base(stringsService, vTubeMonDbConnection, new List<DatabaseWorkItemActionViewModelBase>())
        {
        }

        public override string Table => "inventory_item";
    }
}
