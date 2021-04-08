using System;
using System.Collections.Generic;
using System.Text;

namespace VTubeMon.Wpf.Core.Components
{
    public class DatabaseWorkspace
    {
        public DatabaseWorkspace()
        {
            _queryWorkItems = new List<DatabaseWorkItemModel>();
        }

        private List<DatabaseWorkItemModel> _queryWorkItems { get; }
        public IReadOnlyList<DatabaseWorkItemModel> QueryWorkItems => _queryWorkItems;
        public event EventHandler<DatabaseWorkItemModel> OnNewWorkItem;
        public DatabaseWorkItemModel NewWorkItem(DatabaseWorkItemModel dbQueryWorkItem = null)
        {
            dbQueryWorkItem = dbQueryWorkItem ?? new DatabaseWorkItemModel();
            _queryWorkItems.Add(dbQueryWorkItem);
            OnNewWorkItem?.Invoke(this, dbQueryWorkItem);
            return dbQueryWorkItem;
        }
    }
}
