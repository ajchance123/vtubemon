using System;
using VTubeMon.API.Core.CommandResults;

namespace VTubeMon.API
{
    public interface IVTubeMonCommandFactory
    {
        CommandResult RegisterCommand(ulong user, ulong guild, int registerCash);
        CommandResult DailyCheckinCommand(ulong user, ulong guild, DateTime checkInDateTime);
    }
}
