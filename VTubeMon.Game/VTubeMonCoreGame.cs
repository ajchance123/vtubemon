using System;
using VTubeMon.API;
using VTubeMon.API.Data.Objects;

namespace VTubeMon.Game
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
            return _vTubeMonCoreGameFactories.DailyCheckinCommand(user, guild, checkinTimeUtc, DailyCheckinValue);
        }

        public CommandResult Register(ulong user, ulong guild, bool admin)
        {
            return _vTubeMonCoreGameFactories.RegisterCommand(user, guild, admin, RegistrationValue);
        }

        public int TotalCash(ulong user, ulong guild)
        {
            return _vTubeMonCoreGameFactories.TotalCash(user, guild);
        }

        public CommandResult ToggleAdmin(ulong inituser, ulong user, ulong guild, bool admin)
        {
            IUser userobject = _vTubeMonCoreGameFactories.GetUser(user, guild);
            if(userobject == null)
                return new CommandResult(CommandResultType.NotExist);

            /*IUser userobject = _vTubeMonCoreGameFactories.GetUser(inituser, guild);
            if (!userobject.Admin.Value)
            {
                return new CommandResult(CommandResultType.Unauthorized, "You are not authorized");
            }*/
            return _vTubeMonCoreGameFactories.ToggleAdminCommand(user, guild, admin);
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}
