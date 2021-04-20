using VTubeMon.Data.Objects;

namespace VTubeMon.Data.Commands
{
    public class SelectVTubersCommand : QueryCommand<VTuber>
    {
        public SelectVTubersCommand() : base("vtubers")
        {

        }
    }
}
