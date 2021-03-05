using MySql.Data.MySqlClient;
using System;
using System.Data.Common;

namespace VTubeMon.Data.Objects
{
    public class VTuber : IDataObject
    {
        public int IdVtubers{ get; private set; }
        public string EnName{ get; private set; }
        public string JpName{ get; private set; }
        public string ChannelLink{ get; private set; }
        public DateTime DebutDatetimeUtc{ get; private set; }
        public int Affiliation { get; private set; }
        public bool IsIndependent{ get; private set; }
        public int Generation{ get; private set; }

        public void InitializeFromReader(MySqlDataReader reader)
        {
            IdVtubers = reader.GetInt32("id_vtubers");
            EnName = reader.GetString("en_name");
            JpName = reader.GetString("jp_name");
            ChannelLink = reader.GetString("channel_link");
            DebutDatetimeUtc = reader.GetDateTime("debut_datetime_utc");
            Affiliation = reader.GetInt32("affiliation");
            IsIndependent = reader.GetBoolean("is_independent");
            Generation = reader.GetInt32("generation");
        }
    }
}