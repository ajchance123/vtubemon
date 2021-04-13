using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Linq;
using System.Threading.Tasks;
using VTubeMon.API;
using VTubeMon.Data;
using VTubeMon.Data.Commands;

namespace VTubeMon.Discord
{
    public class VTubeMonCommands
    {
        #region REGULAR FOLK COMMANDS

        [Command("help")]
        public async Task HelpCommand(CommandContext commandContext)
        {
            var dataCache = commandContext.Dependencies.GetDependency<DataCache>();
            var logger = commandContext.Dependencies.GetDependency<ILogger>();

            logger?.Log($"discord.ListCommand({commandContext.Guild.Id}) - start");

            await commandContext.RespondAsync(string.Join(Environment.NewLine, dataCache.VtuberCache.CachedList.Select(v => v.EnName.Value)));

            logger?.Log($"discord.ListCommand({commandContext.Guild.Id}) - end");
        }

        [Command("list")]
        [Description("Prints out vtuber list")]
        public async Task ListCommand(CommandContext commandContext)
        {
            var dataCache = commandContext.Dependencies.GetDependency<DataCache>();
            var logger = commandContext.Dependencies.GetDependency<ILogger>();

            logger?.Log($"discord.ListCommand({commandContext.Guild.Id}) - start");

            await commandContext.RespondAsync(string.Join(Environment.NewLine, dataCache.VtuberCache.CachedList.Select(v=>v.EnName.Value)));

            logger?.Log($"discord.ListCommand({commandContext.Guild.Id}) - end");
        }

        [Command("register")]
        [Description("Register user")]
        public async Task RegisterCommand(CommandContext commandContext)
        {
            try
            {
                var coreGame = commandContext.Dependencies.GetDependency<IVTubeMonCoreGame>();
                var logger = commandContext.Dependencies.GetDependency<ILogger>();

                logger?.Log($"discord.RegisterCommand({commandContext.User.Id}{commandContext.Guild.Id} - start");

                var result = coreGame.Register(commandContext.User.Id, commandContext.Guild.Id, false);

                logger?.Log($"discord.RegisterCommand({commandContext.User.Id}{commandContext.Guild.Id} - {result.ResultType}");

                switch (result.ResultType)
                {
                    case CommandResultType.Success:
                        await commandContext.RespondAsync($"Registration complete! You now have {coreGame.RegistrationValue} vtuber cash!");
                        break;
                    case CommandResultType.Failure:
                        await commandContext.RespondAsync($"Sorry an error has occured {result.Error}, go yell at the devs");
                        break;
                    case CommandResultType.Duplicate:
                        await commandContext.RespondAsync($"You are already registered!");
                        break;
                }

                logger?.Log($"discord.RegisterCommand({commandContext.User.Id}{commandContext.Guild.Id} - end");
            }
            catch (Exception ex)
            {
                await commandContext.RespondAsync(ex.Message);
            }
        }

        [Command("daily")]
        [Description("")]
        public async Task DailyCommand(CommandContext commandContext)
        {
            try
            {
                var coreGame = commandContext.Dependencies.GetDependency<IVTubeMonCoreGame>();
                var logger = commandContext.Dependencies.GetDependency<ILogger>();

                logger?.Log($"discord.DailyCommand({commandContext.User.Id}{commandContext.Guild.Id} - start");

                var result = coreGame.DailyCheckIn(commandContext.User.Id, commandContext.Guild.Id, DateTime.UtcNow);

                logger?.Log($"discord.DailyCommand({commandContext.User.Id}{commandContext.Guild.Id} - {result.ResultType}");

                switch (result.ResultType)
                {
                    case CommandResultType.Success:
                        await commandContext.RespondAsync($"Daily check-in complete! You have gained {coreGame.DailyCheckinValue} vtuber cash!");
                        break;
                    case CommandResultType.Failure:
                        await commandContext.RespondAsync($"Sorry an error has occured {result.Error}, go yell at the devs");
                        break;
                    case CommandResultType.Duplicate:
                        await commandContext.RespondAsync($"You have already checked in today!");
                        break;
                }

                logger?.Log($"discord.DailyCommand({commandContext.User.Id}{commandContext.Guild.Id} - end");
            }
            catch (Exception ex)
            {
                await commandContext.RespondAsync(ex.Message);
            }
        }


        #endregion

        #region ADMIN COMMANDS


        [Command("makeadmin")]
        [Description("Assign specified user admin role")]
        public async Task MakeAdmin(CommandContext commandContext, [Description("Specified user")] DiscordMember member )
        {
            try
            {
                var coreGame = commandContext.Dependencies.GetDependency<IVTubeMonCoreGame>();
                var logger = commandContext.Dependencies.GetDependency<ILogger>();

                logger?.Log($"discord.MakeAdmin({member.Id}{commandContext.Guild.Id} - start");

                var result = coreGame.MakeAdmin(commandContext.User.Id, member.Id, commandContext.Guild.Id, true);

                logger?.Log($"discord.MakeAdmin({member.Id}{commandContext.Guild.Id} - {result.ResultType}");

                switch (result.ResultType)
                {
                    case CommandResultType.Success:
                        await commandContext.RespondAsync($"Congradulations, {member.DisplayName} is now an admin!");
                        break;
                    case CommandResultType.Failure:
                        await commandContext.RespondAsync($"Sorry an error has occured {result.Error}, go yell at the devs");
                        break;
                    case CommandResultType.Duplicate:
                        await commandContext.RespondAsync($"{member.DisplayName} is already an admin!");
                        break;
                    case CommandResultType.Unauthorized:
                        await commandContext.RespondAsync($"You are not authorized to do this.");
                        break;
                }

                logger?.Log($"discord.RegisterCommand({commandContext.User.Id}{commandContext.Guild.Id} - end");
            }
            catch (Exception ex)
            {
                await commandContext.RespondAsync(ex.Message);
            }
        }

        #endregion


        #region SUPER COOL ADMIN COMMANDS - NO COMMON FOLK ALLOWED

        //these commands should check the user id before we actually exeute them
        //(TODO btw)

        [Command("refresh")]
        [Description("Refreshed data cache")]
        public async Task RefreshCommand(CommandContext commandContext)
        {
            var dataCache = commandContext.Dependencies.GetDependency<DataCache>();
            var logger = commandContext.Dependencies.GetDependency<ILogger>();

            dataCache.RefreshAll();
            await commandContext.RespondAsync("Data Refreshed!");
        }

        [Command("ping")]
        [Description("Ping specified user")]
        public async Task PingCommand(CommandContext commandContext, DiscordMember member)
        {
            await commandContext.RespondAsync($"{member.Mention}! Someone wants you!");
        }

        [Command("select")]
        [Description("")]
        public async Task SelectCommand(CommandContext commandContext)
        {
            try
            {
                var dbConnection = commandContext.Dependencies.GetDependency<IVTubeMonDbConnection>();
                var interactivity = commandContext.Dependencies.GetDependency<InteractivityModule>();
                var logger = commandContext.Dependencies.GetDependency<ILogger>();

                var command = new MultiStringSelectCommand($"SELECT {commandContext.RawArgumentString}");

                var discordMessage = await commandContext.RespondAsync(string.Join(Environment.NewLine, dbConnection.ExecuteDbQueryCommand(command).Select((s) => string.Join("\t", s))));
            }
            catch (Exception ex)
            {
                await commandContext.RespondAsync(ex.Message);
            }
        }

        [Command("nonQuery")]
        [Description("")]
        public async Task NonQueryCommand(CommandContext commandContext)
        {
            try
            {
                var dbConnection = commandContext.Dependencies.GetDependency<IVTubeMonDbConnection>();

                //var rows = dbConnection.ExecuteDbNonQueryCommand(new )

                //await commandContext.RespondAsync($"{rows} row(s) affected");
            }
            catch (Exception ex)
            {
                await commandContext.RespondAsync(ex.Message);
            }
        }


        #endregion
    }
}
