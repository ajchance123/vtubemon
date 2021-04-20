using System;

namespace VTubeMon.API
{
    public interface IVTubeMonCoreGame
    {
        int DailyCheckinValue { get; set; }
        int RegistrationValue { get; set; }

        CommandResult DailyCheckIn(ulong user, ulong guild, DateTime checkInTimeUtc);

        CommandResult Register(ulong user, ulong guild, bool admin);
        CommandResult MakeAdmin(ulong inituser, ulong user, ulong guild, bool admin);
        
    }
}
