using System;
using System.Collections.Generic;
using VTubeMon.API;
using VTubeMon.API.Data.Objects;

namespace VTubeMon.Data.Objects
{
    public class Daily : DataObjectBase, IDaily
    {
        public Daily()
        {
            IdUser = new DataProperty<ulong>("id_user", (r) => r.GetUInt64);
            IdGuild = new DataProperty<ulong>("id_guild", (r) => r.GetUInt64);
            CheckInDate = new DateTimeDataProperty("check_in_date");

            DataPropertyList = new List<IDataProperty>()
            {
                IdUser,
                IdGuild,
                CheckInDate
            };
        }

        public Daily(ulong user, ulong guild, DateTime dateTimeUtc)
        {
            IdUser = new DataProperty<ulong>("id_user", (r) => r.GetUInt64, user);
            IdGuild = new DataProperty<ulong>("id_guild", (r) => r.GetUInt64, guild);
            CheckInDate = new DateTimeDataProperty("check_in_date", dateTimeUtc);

            DataPropertyList = new List<IDataProperty>()
            {
                IdUser,
                IdGuild,
                CheckInDate
            };
        }

        public IDataProperty<ulong> IdUser { get; }
        public IDataProperty<ulong> IdGuild { get; }
        public IDataProperty<DateTime> CheckInDate { get; }
        public override IList<IDataProperty> DataPropertyList { get; }
    }
}
