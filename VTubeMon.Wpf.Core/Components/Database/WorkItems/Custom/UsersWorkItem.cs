﻿using System;
using System.Collections.Generic;
using System.Text;
using VTubeMon.API;
using VTubeMon.Wpf.Core.Resources;

namespace VTubeMon.Wpf.Core.Components.Database.WorkItems.Custom
{
    public class UsersWorkItem : DatabaseWorkItemViewModelBase
    {
        public UsersWorkItem(StringsService stringsService, IVTubeMonDbConnection vTubeMonDbConnection) : base(stringsService, vTubeMonDbConnection, new List<DatabaseWorkItemActionViewModelBase>())
        {
        }

        public override string Table => "users";
    }
}
