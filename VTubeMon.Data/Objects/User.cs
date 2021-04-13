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
            Admin = new DataProperty<bool>("admin", (r) => r.GetBoolean);
            VTuberCash = new DataProperty<int>("vtuber_cash", (r) => r.GetInt32);

            DataPropertyList = new List<IDataProperty>()
            {
                IdUser,
                IdGuild,
                Admin,
                VTuberCash
            };
        }
        
        public User(ulong idUser, ulong idGuild, bool admin, int vtuberCash)
        {
            IdUser = new DataProperty<ulong>("id_user", (r) => r.GetUInt64, idUser);
            IdGuild = new DataProperty<ulong>("id_guild", (r) => r.GetUInt64, idGuild);
            Admin = new DataProperty<bool>("admin", (r) => r.GetBoolean, admin);
            VTuberCash = new DataProperty<int>("vtuber_cash", (r) => r.GetInt32, vtuberCash);

            DataPropertyList = new List<IDataProperty>()
            {
                IdUser,
                IdGuild,
                Admin,
                VTuberCash
            };
        }

        public IDataProperty<ulong> IdUser { get; }
        public IDataProperty<ulong> IdGuild { get; }
        public IDataProperty<bool> Admin { get; }
        public IDataProperty<int> VTuberCash { get; }
        public override IList<IDataProperty> DataPropertyList { get; }
    }
}
