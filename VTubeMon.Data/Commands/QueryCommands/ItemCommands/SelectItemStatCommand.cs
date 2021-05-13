using VTubeMon.API.Data.Objects;
using VTubeMon.Common;
using VTubeMon.Data.Objects;

namespace VTubeMon.Data.Commands.QueryCommands
{
    public class SelectItemStatCommand : QueryCommand<ItemStat>
    {
        public SelectItemStatCommand() : base("item_stat") {

        }

        public SelectItemStatCommand(IItem item) : base("item_stat", "*",
            new WhereStatement()
            {
                Equality = Equality.EqualTo,
                Value = $"{item.IdItem.Value}",
                Target = "id_item",
                UseQuotes = false
            })
        {

        }
    }
}
