using VTubeMon.API;
using VTubeMon.Data.Commands;
using VTubeMon.Data.Objects;

namespace VTubeMon.Data
{
    public class DataCache
    {
        public DataCache(IVTubeMonDbConnection vTubeMonDbConnection)
        {
            _vTubeMonDbConnection = vTubeMonDbConnection;
            VtuberCache = new DataCacheList<VTuber>(() => _vTubeMonDbConnection.ExecuteDbSelectCommand(new SelectVTubersCommand()));
            AgencyCache = new DataCacheList<Agency>(() => _vTubeMonDbConnection.ExecuteDbSelectCommand(new SelectAgenciesCommand()));
        }

        private IVTubeMonDbConnection _vTubeMonDbConnection;

        public void RefreshAll()
        {
            VtuberCache.RefreshData();
            AgencyCache.RefreshData();
        }

        public DataCacheList<VTuber> VtuberCache { get; }
        public DataCacheList<Agency> AgencyCache { get; }

    }
}
