using System.Collections.Generic;

namespace VTubeMon.API
{
    public interface IVTubeMonDbConnection
    {
        void CloseConnection();
        IEnumerable<T> ExecuteDbQueryCommand<T>(IDbQueryCommand<T> dbSelectCommand);

        int ExecuteDbNonQueryCommand(IDbNonQueryCommand idbNonQueryCommand);

        void OpenConnection();
    }
}