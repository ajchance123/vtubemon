using System;
using VTubeMon.API;
using VTubeMon.API.Core;
using VTubeMon.API.Data.Objects;

namespace VTubeMon.Core
{
    public class VTubeMonCoreGame : IVTubeMonCoreGame
    {
        public VTubeMonCoreGame(IVTubeMonDbConnection vTubeMonDbConnection, VTubeMonCoreGameFactories vTubeMonCoreGameFactories)
        {
            _vTubeMonDbConnection = vTubeMonDbConnection;
            _vTubeMonCoreGameFactories = vTubeMonCoreGameFactories;
            DailyCheckinValue = 100;
            RegistrationValue = 1000;
        }
        private VTubeMonCoreGameFactories _vTubeMonCoreGameFactories;
        private IVTubeMonDbConnection _vTubeMonDbConnection;
        public int DailyCheckinValue { get; set; }
        public int RegistrationValue { get; set; }

        public DailyCheckinResult DailyCheckIn(ulong user, ulong guild)
        {
            _vTubeMonDbConnection.ExecuteDbNonQueryCommand(_vTubeMonCoreGameFactories.DailyCheckinCommandFactory(user, guild, DateTime.UtcNow));
            var dailies = _vTubeMonDbConnection.ExecuteDbQueryCommand(_vTubeMonCoreGameFactories.DailyQueryCommandFactory(user, guild));
            return new DailyCheckinResult()
            {
                CheckInSuccessfull = true //update to check date time
            };
        }

        public bool Register(ulong user, ulong guild)
        {
            var alreadyRegisterd = isUserAlreadyRegistered(user, guild);
            if(alreadyRegisterd)
            {
                return false;
            }
            else
            {
                var rows = _vTubeMonDbConnection.ExecuteDbNonQueryCommand(_vTubeMonCoreGameFactories.RegisterCommandFactory(user, guild, RegistrationValue));
                return true;
            }
        }

        private bool isUserAlreadyRegistered(ulong user, ulong guild)
        {
            return false; //TODO
        }
    }

    /// <summary>
    /// lets re think this
    /// </summary>
    public class VTubeMonCoreGameFactories
    {
        public VTubeMonCoreGameFactories(
            Func<ulong, ulong, int, IDbNonQueryCommand> registerCommandFactory,
            Func<ulong, ulong, DateTime, IDbNonQueryCommand> dailyCheckinCommandFactory,
            Func<ulong, ulong, IDbQueryCommand<IDaily>> dailyQueryCommandFactory)
        {
            RegisterCommandFactory = registerCommandFactory;
            DailyCheckinCommandFactory = dailyCheckinCommandFactory;
            DailyQueryCommandFactory = dailyQueryCommandFactory;
        }
        public Func<ulong, ulong, int, IDbNonQueryCommand> RegisterCommandFactory { get; }
        public Func<ulong, ulong, DateTime, IDbNonQueryCommand> DailyCheckinCommandFactory { get; }
        public Func<ulong, ulong, IDbQueryCommand<IDaily>> DailyQueryCommandFactory { get; }
    }
}
