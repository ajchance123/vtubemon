using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using VTubeMon.API;

namespace VTubeMon.MySql
{
    public class VTubeMonMySqlConnection : IVTubeMonDbConnection
    {
        private const string CONNECTION_STRING = "server=127.0.0.1;uid=VTubeAdmin;" +
                "pwd=vtuber123#W33B;database=vtube_mon_db";

        private MySqlConnection _connection;
        private ILogger _logger;

        public VTubeMonMySqlConnection(ILogger logger = null)
        {
            _logger = logger;
        }

        public void OpenConnection()
        {
            if (_connection != null) return; //already conneted

            _connection = new MySqlConnection();
            _connection.ConnectionString = CONNECTION_STRING;
            _connection.Open();
            _logger?.Log("DB Connection Opened");
        }

        public void CloseConnection()
        {
            _connection.Close();
            _connection.Dispose();
            _connection = null;
            _logger?.Log("DB Connection Closed");
        }

        public IEnumerable<T> ExecuteDbQueryCommand<T>(IDbQueryCommand<T> dbSelectCommand)
        {
            _logger?.Log(dbSelectCommand.Statement);
            try
            {
                if (dbSelectCommand.Statement == null) return null; //maybe throw ?

                MySqlCommand mySqlCommand = new MySqlCommand(dbSelectCommand.Statement, _connection);
                var mySqlReader = mySqlCommand.ExecuteReader();
                MySqlDataReaderWrapper reader = new MySqlDataReaderWrapper(mySqlReader);
                return dbSelectCommand.ReadData(reader);
            }
            catch(Exception ex)
            {
                _logger?.Log(ex.Message);
                throw ex;
            }
        }

        public int ExecuteDbNonQueryCommand<T>(IDbNonQueryCommand<T> dbNonQueryCommand)
            where T : IDataObject
        {
            try
            {
                if (dbNonQueryCommand.DataObjects == null || dbNonQueryCommand.DataObjects.Count() == 0) return 0; //maybe throw ?

                _logger?.Log(dbNonQueryCommand.Statement);
                MySqlCommand mySqlCommand = new MySqlCommand(dbNonQueryCommand.Statement, _connection);
                return mySqlCommand.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                _logger?.Log(ex.Message);
                throw ex;
            }
        }


        public int ExecuteDbNonQueryCommand(IDbNonQueryCommand dbNonQueryCommand)
        {
            try
            {
                _logger?.Log(dbNonQueryCommand.Statement);

                if (dbNonQueryCommand.Statement == null) return 0; //maybe throw ?

                MySqlCommand mySqlCommand = new MySqlCommand(dbNonQueryCommand.Statement, _connection);
                return mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _logger?.Log(ex.Message);
                throw ex;
            }
        }
    }
}
