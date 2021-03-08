using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using VTubeMon.API;

namespace VTubeMon.MySql
{
    public class VTubeMonDbConnection : IVTubeMonDbConnection
    {
        private const string CONNECTION_STRING = "server=127.0.0.1;uid=VTubeAdmin;" +
                "pwd=vtuber123#W33B;database=vtube_mon_db";

        private MySqlConnection _connection;
        public VTubeMonDbConnection()
        {

        }

        public void OpenConnection()
        {
            if (_connection != null) return; //already conneted

            _connection = new MySqlConnection();
            _connection.ConnectionString = CONNECTION_STRING;
            _connection.Open();
        }

        public void CloseConnection()
        {
            _connection.Close();
            _connection.Dispose();
            _connection = null;
        }

        public IEnumerable<T> ExecuteDbSelectCommand<T>(IDbSelectCommand<T> dbSelectCommand)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(dbSelectCommand.Statement, _connection);
            var mySqlReader = mySqlCommand.ExecuteReader();
            MySqlDataReaderWrapper reader = new MySqlDataReaderWrapper(mySqlReader);
            return dbSelectCommand.ReadData(reader);
        }

        public int InsertDbCommand<T>(string table, params T[] dataObjects)
            where T : IDataObject
        {
            if (dataObjects == null || dataObjects.Length == 0) return 0;

            var first = dataObjects.First();

            string statement = $"INSERT INTO {table}{first.ColumnNames} VALUES {string.Join(",", dataObjects.Select(d => d.Values))}";

            MySqlCommand mySqlCommand = new MySqlCommand(statement, _connection);
            return mySqlCommand.ExecuteNonQuery();
        }
    }
}
