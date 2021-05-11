using VTubeMon.API.Data.Objects;
using VTubeMon.Common;

namespace VTubeMon.Data.Commands.QueryCommands
{
    public class SelectItemStatCommand : QueryCommand<UserSettingsValue>
    {
        public SelectItemStatCommand() : base("item_stat") {

        }

        public SelectItemStatCommand(IItem item) : base("item_stat", "*",
            new WhereStatement()
            {
                Equality = Equality.EqualTo,
                Value = $"{item.IdItem}",
                Target = "id_item",
                UseQuotes = false
            })
        {

        }
    }
}
