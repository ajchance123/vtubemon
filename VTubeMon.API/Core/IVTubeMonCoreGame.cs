using System;
using VTubeMon.API.Core.CommandResults;

namespace VTubeMon.API.Core
{
    public interface IVTubeMonCoreGame
    {
        int DailyCheckinValue { get; set; }
        int RegistrationValue { get; set; }

        CommandResult DailyCheckIn(ulong user, ulong guild, DateTime checkInTimeUtc);

        CommandResult Register(ulong user, ulong guild);
    }
}
