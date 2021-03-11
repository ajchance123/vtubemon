using System;
using System.Collections.Generic;
using System.Text;

namespace VTubeMon.Wpf.Core.Components
{
    public class DatabaseWorkspace
    {
        public DatabaseWorkspace()
        {
            _queryWorkItems = new List<DbQueryWorkItem>();
        }

        private List<DbQueryWorkItem> _queryWorkItems { get; }
        public IReadOnlyList<DbQueryWorkItem> QueryWorkItems => _queryWorkItems;
        public event EventHandler<DbQueryWorkItem> OnNewWorkItem;
        public DbQueryWorkItem NewWorkItem(DbQueryWorkItem dbQueryWorkItem = null)
        {
            dbQueryWorkItem = dbQueryWorkItem ?? new DbQueryWorkItem();
            _queryWorkItems.Add(dbQueryWorkItem);
            OnNewWorkItem?.Invoke(this, dbQueryWorkItem);
            return dbQueryWorkItem;
        }
    }
}
