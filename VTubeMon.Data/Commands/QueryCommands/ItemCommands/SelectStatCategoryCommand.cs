using VTubeMon.Data.Objects;

namespace VTubeMon.Data.Commands.QueryCommands.ItemCommands
{
    public class SelectStatCategoryCommand : QueryCommand<ItemCategory>
    {
        public SelectStatCategoryCommand() : base("stat_category")
        {

        }
    }
}
