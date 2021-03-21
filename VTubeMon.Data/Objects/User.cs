using System.Collections.Generic;
using VTubeMon.API;
using VTubeMon.API.Data.Objects;

namespace VTubeMon.Data.Objects
{
    public class User : DataObjectBase, IUser
    {
        public User()
        {
            IdUser = new DataProperty<ulong>("id_user", (r) => r.GetUInt64);
            IdGuild = new DataProperty<ulong>("id_guild", (r) => r.GetUInt64);
            VTuberCash = new DataProperty<int>("vtuber_cash", (r) => r.GetInt32);

            DataPropertyList = new List<IDataProperty>()
            {
                IdUser,
                IdGuild,
                VTuberCash
            };
        }
        
        public User(ulong idUser, ulong idGuild, int vtuberCash)
        {
            IdUser = new DataProperty<ulong>("id_user", (r) => r.GetUInt64, idUser);
            IdGuild = new DataProperty<ulong>("id_guild", (r) => r.GetUInt64, idGuild);
            VTuberCash = new DataProperty<int>("vtuber_cash", (r) => r.GetInt32, vtuberCash);

            DataPropertyList = new List<IDataProperty>()
            {
                IdUser,
                IdGuild,
                VTuberCash
            };
        }

        public IDataProperty<ulong> IdUser { get; }
        public IDataProperty<ulong> IdGuild { get; }
        public IDataProperty<int> VTuberCash { get; }
        public override IList<IDataProperty> DataPropertyList { get; }
    }
}
