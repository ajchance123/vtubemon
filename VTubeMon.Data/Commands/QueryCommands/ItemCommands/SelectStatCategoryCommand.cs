using VTubeMon.API.Data.Objects;
using VTubeMon.Common;
using VTubeMon.Data.Objects;

namespace VTubeMon.Data.Commands.QueryCommands
{
    public class SelectStatCategoryCommand : QueryCommand<StatCategory>
    {
        public SelectStatCategoryCommand() : base("stat_category")
        {

        }

        public SelectStatCategoryCommand(int idStat) : base("stat_category", "*",
            new WhereStatement()
            {
                Equality = Equality.EqualTo,
                Value = $"{idStat}",
                Target = "id_stat",
                UseQuotes = false
            })
        {

        }
    }
}
