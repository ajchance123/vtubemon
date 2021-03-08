using System.Collections.Generic;
using VTubeMon.API;
using VTubeMon.Data.Objects;

namespace VTubeMon.Data.Commands
{
    public class SelectVTubersCommand : IDbSelectCommand<VTuber>
    {
        public string Statement => "SELECT * FROM vtubers";

        public IEnumerable<VTuber> ReadData(INamedDataReader namedDataReader)
        {
            using (namedDataReader)
            {
                while (namedDataReader.Read())
                {
                    var vtuber = new VTuber();
                    vtuber.InitializeFromReader(namedDataReader);
                    yield return vtuber;
                }
            }
        }
    }
}
