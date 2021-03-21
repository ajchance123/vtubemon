using System;
using VTubeMon.API;
using VTubeMon.API.Core;
using VTubeMon.API.Core.CommandResults;

namespace VTubeMon.Core
{
    public class VTubeMonCoreGame : IVTubeMonCoreGame
    {
        public VTubeMonCoreGame(IVTubeMonCommandFactory vTubeMonCoreGameFactories)
        {
            _vTubeMonCoreGameFactories = vTubeMonCoreGameFactories;
            DailyCheckinValue = 100;
            RegistrationValue = 1000;
        }
        private IVTubeMonCommandFactory _vTubeMonCoreGameFactories;

        public int DailyCheckinValue { get; set; }
        public int RegistrationValue { get; set; }

        public CommandResult DailyCheckIn(ulong user, ulong guild, DateTime checkinTimeUtc)
        {
            return _vTubeMonCoreGameFactories.DailyCheckinCommand(user, guild, checkinTimeUtc);
        }

        public CommandResult Register(ulong user, ulong guild)
        {
            return _vTubeMonCoreGameFactories.RegisterCommand(user, guild, RegistrationValue);
        }
    }
}
