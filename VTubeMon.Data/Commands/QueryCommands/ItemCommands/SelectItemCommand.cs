using VTubeMon.Data.Objects;

namespace VTubeMon.Data.Commands.QueryCommands
{
    public class SelectItemCommand : QueryCommand<Item>
    {
        public SelectItemCommand() : base("item")
        {

        }
    }
}
