﻿using System.Collections.Generic;
using VTubeMon.API;
using VTubeMon.Wpf.Core.Resources;

namespace VTubeMon.Wpf.Core.Components.Database.WorkItems.Custom
{
    public class ItemStatWorkItem : DatabaseWorkItemViewModelBase
    {
        public ItemStatWorkItem(StringsService stringsService, IVTubeMonDbConnection vTubeMonDbConnection) : base(stringsService, vTubeMonDbConnection, new List<DatabaseWorkItemActionViewModelBase>())
        {
        }
        public override string Table => "item_stat";
    }
}
