using VTubeMon.API.Data.Objects;
using VTubeMon.Common;
using VTubeMon.Data.Objects;

namespace VTubeMon.Data.Commands.QueryCommands.ItemCommands
{
    public class SelectItemCategoryCommand : QueryCommand<ItemCategory>
    {
        public SelectItemCategoryCommand() : base("item_category")
        {

        }

        public SelectItemCategoryCommand(int idCategory) : base("item_category", "*",
            new WhereStatement()
            {
                Equality = Equality.EqualTo,
                Value = $"{idCategory}",
                Target = "id_category",
                UseQuotes = false
            })
        {

        }
    }
}
