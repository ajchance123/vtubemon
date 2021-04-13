using System;
using VTubeMon.API.Data.Objects;

namespace VTubeMon.API
{
    public interface IVTubeMonCommandFactory
    {
        CommandResult RegisterCommand(ulong user, ulong guild, bool admin, int registerCash);
        CommandResult DailyCheckinCommand(ulong user, ulong guild, DateTime checkInDateTime);
        CommandResult MakeAdminCommand(ulong user, ulong guild, bool admin);
        IUser GetUser(ulong user, ulong guild);
    }
}
