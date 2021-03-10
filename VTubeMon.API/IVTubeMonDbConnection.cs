using System.Collections.Generic;

namespace VTubeMon.API
{
    public interface IVTubeMonDbConnection
    {
        void CloseConnection();
        IEnumerable<T> ExecuteDbSelectCommand<T>(IDbSelectCommand<T> dbSelectCommand);

        int InsertDbCommand<T>(string table, params T[] dataObjects)
            where T : IDataObject;

        int ExecuteNonQuery(string statement);
        void OpenConnection();
    }
}