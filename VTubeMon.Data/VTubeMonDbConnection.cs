using System;
using System.Collections.Generic;
using MySql;
using MySql.Data.MySqlClient;
using VTubeMon.Data.Objects;

namespace VTubeMon.Data
{
    public class VTubeMonDbConnection
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

            _connection = new MySql.Data.MySqlClient.MySqlConnection();
            _connection.ConnectionString = CONNECTION_STRING;
            _connection.Open();
        }

        public void CloseConnection()
        {
            _connection.Close();
            _connection.Dispose();
            _connection = null;
        }

        public IList<VTuber> ReadVTubers()
        {
            IList<VTuber> vtubers = new List<VTuber>();

            MySqlCommand command = new MySqlCommand("SELECT * FROM vtubers", _connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var vtuber = new VTuber();
                    vtuber.InitializeFromReader(reader);
                    vtubers.Add(vtuber);
                }
            }

            return vtubers;
        }

        public IList<Agency> ReadAgencies()
        {
            IList<Agency> agencies = new List<Agency>();

            MySqlCommand command = new MySqlCommand("SELECT * FROM agencies", _connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var agency = new Agency();
                    agency.InitializeFromReader(reader);
                    agencies.Add(agency);
                }
            }

            return agencies;
        }


    }
}
