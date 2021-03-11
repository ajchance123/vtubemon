using System.Collections.Generic;
using VTubeMon.API;
using VTubeMon.Data.Objects;

namespace VTubeMon.Data.Commands
{
    public class SelectAgenciesCommand : IDbQueryCommand<Agency>
    {
        public string Statement => "SELECT * FROM agencies";

        public IEnumerable<Agency> ReadData(INamedDataReader namedDataReader)
        {
            using (namedDataReader)
            {
                while (namedDataReader.Read())
                {
                    var agency = new Agency();
                    agency.InitializeFromReader(namedDataReader);
                    yield return agency;
                }
            }
        }
    }
}
