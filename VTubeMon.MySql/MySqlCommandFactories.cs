using System;
using System.Linq;
using VTubeMon.API;
using VTubeMon.API.Core.CommandResults;
using VTubeMon.API.Data.Objects;
using VTubeMon.Data.Commands;
using VTubeMon.Data.Objects;

namespace VTubeMon.MySql
{
    public class MySqlCommandFactories : IVTubeMonCommandFactory
    {
        public MySqlCommandFactories(IVTubeMonDbConnection vTubeMonDbConnection)
        {
            _vTubeMonDbConnection = vTubeMonDbConnection;
        }

        private IVTubeMonDbConnection _vTubeMonDbConnection;
        public CommandResult RegisterCommand(ulong user, ulong guild, int registerCash)
        {
            try
            {
                int rows = _vTubeMonDbConnection.ExecuteDbNonQueryCommand(new InsertCommand<IUser>("users", new User(user, guild, registerCash)) { Ignore = true });
                return new CommandResult(rows > 0 ? CommandResultType.Success : CommandResultType.Duplicate);
            }
            catch(Exception ex)
            {
                return ex;
            }
        }

        public CommandResult DailyCheckinCommand(ulong user, ulong guild, DateTime checkInDateTime)
        {
            var queryDailiesCommand = new QueryCommand<Daily>("dailies", "*",
                new WhereStatement()
                {
                    Equality = Core.Equality.EqualTo,
                    Value = $"{user}",
                    Target = "id_user",
                    UseQuotes = false
                },
                new WhereStatement()
                {
                    Equality = Core.Equality.EqualTo,
                    Value = $"{guild}",
                    Target = "id_guild",
                    UseQuotes = false
                });

            var dailies = _vTubeMonDbConnection.ExecuteDbQueryCommand(queryDailiesCommand);
            var lastCheckIn = dailies.Last();
            if(lastCheckIn.CheckInDate.Value.DayOfYear == DateTime.Now.DayOfYear)
            {
                //already checked in
                return new CommandResult(CommandResultType.Duplicate);
            }
            bool insertResult;
            try
            {
                insertResult = _vTubeMonDbConnection.ExecuteDbNonQueryCommand(new InsertCommand<IDaily>("dailies", new Daily(user, guild, checkInDateTime))) > 0;
            }
            catch(Exception ex)
            {
                return ex;
            }

            return new CommandResult(CommandResultType.Success);
        }

        public int TotalCash(ulong user, ulong guild)
        {

            var userQueryCommand = new QueryCommand<User>("users", "*",
                new WhereStatement()
                {
                    Equality = Core.Equality.EqualTo,
                    Target = "id_user",
                    Value = $"{user}"
                },
                new WhereStatement()
                {
                    Equality = Core.Equality.EqualTo,
                    Target = "id_guild",
                    Value = $"{guild}"
                });

            var users = _vTubeMonDbConnection.ExecuteDbQueryCommand(userQueryCommand);

            return users.Single().VTuberCash.Value;
        }
    }

}
