using VTubeMon.API.Data.Objects;
using VTubeMon.Common;
using VTubeMon.Data.Objects;

namespace VTubeMon.Data.Commands.QueryCommands
{
    public class SelectItemCommand : QueryCommand<Item>
    {
        public SelectItemCommand() : base("item")
        {

        }

        public SelectItemCommand(int idCategory) : base("item", "*",
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
