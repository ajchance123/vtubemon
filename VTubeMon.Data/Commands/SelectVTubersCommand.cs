using System.Collections.Generic;
using VTubeMon.API;
using VTubeMon.Data.Objects;

namespace VTubeMon.Data.Commands
{
    public class SelectVTubersCommand : SelectCommandBase<VTuber>
    {
        protected override string Table => "vtubers";
    }
}
