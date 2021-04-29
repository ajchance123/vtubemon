using System;
using System.Collections.Generic;
using System.Linq;
using VTubeMon.API;
using VTubeMon.API.Data.Objects;
using VTubeMon.Common;
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

        public IUser GetUser(ulong user, ulong guild)
        {
            QueryCommand<User> command = new QueryCommand<User>("users", "*",
                new WhereStatement()
                {
                    Equality = Equality.EqualTo,
                    Value = $"{user}",
                    Target = "id_user",
                    UseQuotes = false
                },
                new WhereStatement()
                {
                    Equality = Equality.EqualTo,
                    Value = $"{guild}",
                    Target = "id_guild",
                    UseQuotes = false
                });

            List<User> user_list = _vTubeMonDbConnection.ExecuteDbQueryCommand(command).ToList();

            if (!user_list.Any())
            {
                return null;
            }

            return user_list.First();
        }

        private IVTubeMonDbConnection _vTubeMonDbConnection;
        public CommandResult RegisterCommand(ulong user, ulong guild, bool admin, int registerCash)
        {
            try
            {
                int rows = _vTubeMonDbConnection.ExecuteDbNonQueryCommand(new InsertCommand<IUser>("users", new User(user, guild, admin, registerCash)) { Ignore = true });
                return new CommandResult(rows > 0 ? CommandResultType.Success : CommandResultType.Duplicate);
            }
            catch(Exception ex)
            {
                return ex;
            }
        }

        public CommandResult DailyCheckinCommand(ulong user, ulong guild, DateTime checkInDateTime, int dailyCash)
        {
            if(GetUser(user, guild) == null)
                return new CommandResult(CommandResultType.NotExist);

            var queryDailiesCommand = new QueryCommand<Daily>("dailies", "*",
                new WhereStatement()
                {
                    Equality = Equality.EqualTo,
                    Value = $"{user}",
                    Target = "id_user",
                    UseQuotes = false
                },
                new WhereStatement()
                {
                    Equality = Equality.EqualTo,
                    Value = $"{guild}",
                    Target = "id_guild",
                    UseQuotes = false
                });

           List<Daily> dailies = _vTubeMonDbConnection.ExecuteDbQueryCommand(queryDailiesCommand).ToList();

            if(dailies.Any())
            {
                var lastCheckIn = dailies.Last();
                if (lastCheckIn.CheckInDate.Value.DayOfYear == DateTime.Now.DayOfYear)
                {
                    //already checked in
                    return new CommandResult(CommandResultType.Duplicate);
                }
            }

            int newCash = TotalCash(user, guild) + dailyCash;

            NonQueryCommand updateCash = new NonQueryCommand("users", $"UPDATE `users` SET vtuber_cash = {newCash} WHERE id_user = {user} AND id_guild = {guild};");
            _vTubeMonDbConnection.ExecuteDbNonQueryCommand(updateCash);
            _vTubeMonDbConnection.ExecuteDbNonQueryCommand(new InsertCommand<IDaily>("dailies", new Daily(user, guild, checkInDateTime)));

            return new CommandResult(CommandResultType.Success);
        }

        public int TotalCash(ulong user, ulong guild)
        {
            var userQueryCommand = new QueryCommand<User>("users", "*",
                new WhereStatement()
                {
                    Equality = Equality.EqualTo,
                    Target = "id_user",
                    Value = $"{user}"
                },
                new WhereStatement()
                {
                    Equality = Equality.EqualTo,
                    Target = "id_guild",
                    Value = $"{guild}"
                });

            List<User> users = new List<User>(_vTubeMonDbConnection.ExecuteDbQueryCommand(userQueryCommand));

            try
            {
                return users.Single().VTuberCash.Value;
            }
            catch
            {
                throw new Exception("User does not exist");
            }
        }

        public CommandResult ToggleAdminCommand(ulong user, ulong guild, bool admin)
        {
            NonQueryCommand updateAdmin = new NonQueryCommand("users", $"UPDATE `users` SET admin = {admin} WHERE id_user = {user} AND id_guild = {guild};");

            var users = _vTubeMonDbConnection.ExecuteDbNonQueryCommand(updateAdmin);

            return new CommandResult(users > 0 ? CommandResultType.Success : CommandResultType.Failure);
        }
    }

}
