using VTubeMon.Data.Objects;

namespace VTubeMon.Data
{
    public class DataCache
    {
        public DataCache(VTubeMonDbConnection vTubeMonDbConnection)
        {
            _vTubeMonDbConnection = vTubeMonDbConnection;
            VtuberCache = new DataCacheList<VTuber>(_vTubeMonDbConnection.ReadVTubers);
            AgencyCache = new DataCacheList<Agency>(_vTubeMonDbConnection.ReadAgencies);
            RefreshAll();
        }

        private VTubeMonDbConnection _vTubeMonDbConnection;

        public void RefreshAll()
        {
            VtuberCache.RefreshData();
            AgencyCache.RefreshData();
        }

        public DataCacheList<VTuber> VtuberCache { get; }
        public DataCacheList<Agency> AgencyCache { get; }

    }
}
