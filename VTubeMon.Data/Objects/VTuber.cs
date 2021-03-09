using System;
using System.Collections.Generic;
using VTubeMon.API;

namespace VTubeMon.Data.Objects
{
    public class VTuber : DataObjectBase
    {
        public VTuber()
        {
            IdVtubers = new DataProperty<int>("id_vtubers", (r) => r.GetInt32);
            EnName = new StringDataProperty("en_name");
            JpName = new StringDataProperty("jp_name");
            ChannelLink = new StringDataProperty("channel_link");
            DebutDatetimeUtc = new DateTimeDataProperty("debut_datetime_utc");
            Affiliation = new DataProperty<int>("affiliation", (r) => r.GetInt32);
            IsIndependent = new DataProperty<bool>("is_independent", (r) => r.GetBoolean);
            Generation = new DataProperty<int>("generation", (r) => r.GetInt32);

            DataPropertyList = new List<IDataProperty>()
            {
                IdVtubers, EnName, JpName, ChannelLink, DebutDatetimeUtc, Affiliation, IsIndependent, Generation
            };
        }

        public DataProperty<int> IdVtubers{ get; private set; }
        public DataProperty<string> EnName { get; private set; }
        public DataProperty<string> JpName { get; private set; }
        public DataProperty<string> ChannelLink{ get; private set; }
        public DataProperty<DateTime> DebutDatetimeUtc{ get; private set; }
        public DataProperty<int> Affiliation { get; private set; }
        public DataProperty<bool> IsIndependent{ get; private set; }
        public DataProperty<int> Generation{ get; private set; }

        public override IList<IDataProperty> DataPropertyList { get; }

    }
}