using VTubeMon.Data.Objects;

namespace VTubeMon.Data.Commands.QueryCommands.ItemCommands
{
    public class SelectItemCategoryCommand : QueryCommand<ItemCategory>
    {
        public SelectItemCategoryCommand() : base("item_category")
        {

        }
    }
}
