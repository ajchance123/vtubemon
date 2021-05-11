using System.Collections.Generic;
using VTubeMon.API;
using VTubeMon.API.Data.Objects;

namespace VTubeMon.Data.Objects
{
    public class StatCategory : DataObjectBase, IStatCategory
    {
        public StatCategory()
        {
            IdStat = new DataProperty<int>("id_stat", (r) => r.GetInt32);
            StatName = new DataProperty<string>("stat_name", (r) => r.GetString);

            DataPropertyList = new List<IDataProperty>()
            {
                IdStat,
                StatName
            };
        }

        public StatCategory(int idStat, string statName)
        {
            IdStat = new DataProperty<int>("id_stat", (r) => r.GetInt32, idStat);
            StatName = new DataProperty<string>("stat_name", (r) => r.GetString, statName);

            DataPropertyList = new List<IDataProperty>()
            {
                IdStat,
                StatName
            };
        }

        public IDataProperty<int> IdStat { get; }
        public IDataProperty<string> StatName { get; }
        public override IList<IDataProperty> DataPropertyList { get; }
    }
}
