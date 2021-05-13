using System;
using System.Collections.Generic;
using VTubeMon.API;
using VTubeMon.API.Data.Objects;

namespace VTubeMon.Data.Objects
{
    public class ItemStat : DataObjectBase, IItemStat
    {
        public ItemStat()
        {
            IdItem = new DataProperty<int>("id_item", (r) => r.GetInt32);
            IdStat = new DataProperty<int>("id_stat", (r) => r.GetInt32);
            StatValue = new DataProperty<int>("stat_value", (r) => r.GetInt32);

            DataPropertyList = new List<IDataProperty>()
            {
                IdItem,
                IdStat,
                StatValue
            };
        }

        public ItemStat(int idItem, int idStat, int statValue)
        {
            IdItem = new DataProperty<int>("id_item", (r) => r.GetInt32, idItem);
            IdStat = new DataProperty<int>("id_stat", (r) => r.GetInt32, idStat);
            StatValue = new DataProperty<int>("stat_value", (r) => r.GetInt32, statValue);

            DataPropertyList = new List<IDataProperty>()
            {
                IdItem,
                IdStat,
                StatValue
            };
        }

        public override IList<IDataProperty> DataPropertyList { get; }
        public IDataProperty<int> IdItem { get; }
        public IDataProperty<int> IdStat { get; }
        public IDataProperty<int> StatValue { get; }
    }
}
