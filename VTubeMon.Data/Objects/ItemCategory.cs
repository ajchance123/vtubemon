using System.Collections.Generic;
using VTubeMon.API;
using VTubeMon.API.Data.Objects;

namespace VTubeMon.Data.Objects
{
    public class ItemCategory : DataObjectBase, IItemCategory
    {
        public ItemCategory()
        {
            IdCategory = new DataProperty<int>("id_category", (r) => r.GetInt32);
            CategoryName = new DataProperty<string>("category_name", (r) => r.GetString);

            DataPropertyList = new List<IDataProperty>()
            {
                IdCategory,
                CategoryName
            };
        }

        public ItemCategory(int idCategory, string categoryName)
        {
            IdCategory = new DataProperty<int>("id_category", (r) => r.GetInt32, idCategory);
            CategoryName = new DataProperty<string>("category_name", (r) => r.GetString, categoryName);
        }

        public IDataProperty<int> IdCategory { get; }
        public IDataProperty<string> CategoryName { get; }
        public override IList<IDataProperty> DataPropertyList { get; }
    }
}
