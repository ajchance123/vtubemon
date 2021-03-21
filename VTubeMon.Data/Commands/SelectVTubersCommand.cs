using VTubeMon.Data.Objects;

namespace VTubeMon.Data.Commands
{
    public class SelectVTubersCommand : QueryCommand<VTuber>
    {
        protected override string Table => "vtubers";
    }
}
