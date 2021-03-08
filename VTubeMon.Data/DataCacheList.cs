using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VTubeMon.Data
{
    public class DataCacheList<T> 
    {
        public DataCacheList(Func<IEnumerable<T>> listFactory)
        {
            _listFactory = listFactory;
        }

        private Func<IEnumerable<T>> _listFactory;

        public IReadOnlyList<T> CachedList { get; private set; }
        public event EventHandler<IReadOnlyList<T>> OnDataRefreshed;
        public void RefreshData()
        {
            CachedList = new List<T>(_listFactory());
            Task.Run(() =>
            {
                OnDataRefreshed?.Invoke(this, CachedList);
            });
        }
    }
}
