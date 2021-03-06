using System;
using System.Collections.Generic;
using System.Text;

namespace VTubeMon.Data
{
    public class DataCacheList<T> 
    {
        public DataCacheList(Func<IList<T>> listFactory)
        {
            _listFactory = listFactory;
        }

        private Func<IList<T>> _listFactory;

        public IReadOnlyList<T> CachedList { get; private set; }
        public event EventHandler<IReadOnlyList<T>> OnDataRefreshed;
        public void RefreshData()
        {
            CachedList = new List<T>(_listFactory());
            OnDataRefreshed?.Invoke(this, CachedList);
        }
    }
}
