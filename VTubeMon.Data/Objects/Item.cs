using System;
using System.Collections.Generic;
using VTubeMon.API;
using VTubeMon.API.Data.Objects;

namespace VTubeMon.Data.Objects
{
    public class Item : DataObjectBase, IItem
    {
        public Item()
        {
            IdItem = new DataProperty<int>("id_item", (r) => r.GetInt32);
            ItemName = new DataProperty<string>("item_name", (r) => r.GetString);
            IdCategory = new DataProperty<int>("id_category", (r) => r.GetInt32);
            Price = new DataProperty<int>("price", (r) => r.GetInt32);

            DataPropertyList = new List<IDataProperty>()
            {
                IdItem,
                ItemName,
                IdCategory,
                Price
            };
        }

        public Item(int idItem, string itemName, int idCategory, string categoryName, int price, int itemBuyLimit,int itemQuantity)
        {
            IdItem = new DataProperty<int>("id_item", (r) => r.GetInt32, idItem);
            ItemName = new DataProperty<string>("item_name", (r) => r.GetString, itemName);
            IdCategory = new DataProperty<int>("id_category", (r) => r.GetInt32, idCategory);
            Price = new DataProperty<int>("price", (r) => r.GetInt32, price);


            DataPropertyList = new List<IDataProperty>()
            {
                IdItem,
                ItemName,
                IdCategory,
                Price
            };
        }

        public IDataProperty<int> IdItem { get; }
        public IDataProperty<string> ItemName { get; }
        public IDataProperty<int> IdCategory { get; }
        public IDataProperty<int> Price { get; }
        public override IList<IDataProperty> DataPropertyList { get; }
    }
}
