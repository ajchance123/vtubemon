using VTubeMon.API;

namespace VTubeMon.Data.Commands
{
    public class NonQueryCommand : IDbNonQueryCommand
    {

        public NonQueryCommand(string table, string statement)
        {
            Table = table;
            Statement = statement;
        }

        public string Table { get; }

        public string Statement { get; }
    }
}
