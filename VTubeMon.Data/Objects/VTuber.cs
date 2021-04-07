using System;
using System.Collections.Generic;
using VTubeMon.API;
using VTubeMon.API.Data.Objects;

namespace VTubeMon.Data.Objects
{
    public class VTuber : DataObjectBase, IVTuber
    {
        public VTuber()
        {
            IdVtubers = new DataProperty<int>("id_vtubers", (r) => r.GetInt32);
            EnName = new StringDataProperty("en_name");
            JpName = new StringDataProperty("jp_name");
            ChannelLink = new StringDataProperty("channel_link");
            DebutDatetimeUtc = new DateTimeDataProperty("debut_datetime_utc");
            IdAgency = new DataProperty<int>("id_agency", (r) => r.GetInt32);
            IsIndependent = new DataProperty<bool>("is_independent", (r) => r.GetBoolean);
            Generation = new DataProperty<int>("generation", (r) => r.GetInt32);

            DataPropertyList = new List<IDataProperty>()
            {
                IdVtubers, EnName, JpName, ChannelLink, DebutDatetimeUtc, IdAgency, IsIndependent, Generation
            };
        }

        public IDataProperty<int> IdVtubers{ get; private set; }
        public IDataProperty<string> EnName { get; private set; }
        public IDataProperty<string> JpName { get; private set; }
        public IDataProperty<string> ChannelLink{ get; private set; }
        public IDataProperty<DateTime> DebutDatetimeUtc{ get; private set; }
        public IDataProperty<int> IdAgency { get; private set; }
        public IDataProperty<bool> IsIndependent{ get; private set; }
        public IDataProperty<int> Generation{ get; private set; }

        public override IList<IDataProperty> DataPropertyList { get; }

    }
}