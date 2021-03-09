using System.Collections.Generic;
using VTubeMon.API;

namespace VTubeMon.Data.Objects
{
    public class User : DataObjectBase
    {
        public User()
        {
            IdUser = new DataProperty<ulong>("id_user", (r) => r.GetUInt64);
            IdGuild = new DataProperty<ulong>("id_guild", (r) => r.GetUInt64);
        }

        public DataProperty<ulong> IdUser { get; }
        public DataProperty<ulong> IdGuild { get; }
        public override IList<IDataProperty> DataPropertyList { get; }
    }
}
