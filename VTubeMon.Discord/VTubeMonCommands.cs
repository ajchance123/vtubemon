using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Linq;
using System.Threading.Tasks;
using VTubeMon.API;
using VTubeMon.API.Core;
using VTubeMon.Data;
using VTubeMon.Data.Commands;

namespace VTubeMon.Discord
{
    public class VTubeMonCommands
    {
        #region REGULAR FOLK COMMANDS

        [Command("list")]
        public async Task ListCommand(CommandContext commandContext)
        {
            var dataCache = commandContext.Dependencies.GetDependency<DataCache>();
            var logger = commandContext.Dependencies.GetDependency<ILogger>();

            logger?.Log($"discord.ListCommand({commandContext.Guild.Id}) - start");

            foreach (var vtuber in dataCache.VtuberCache.CachedList)
            {
                await commandContext.RespondAsync(vtuber.EnName.Value);
            }

            logger?.Log($"discord.ListCommand({commandContext.Guild.Id}) - end");
        }

        [Command("register")]
        public async Task RegisterCommand(CommandContext commandContext)
        {
            try
            {
                var coreGame = commandContext.Dependencies.GetDependency<IVTubeMonCoreGame>();
                var logger = commandContext.Dependencies.GetDependency<ILogger>();

                logger?.Log($"discord.RegisterCommand({commandContext.User.Id}, {commandContext.Guild.Id}) - start");
                var result = coreGame.Register(commandContext.User.Id, commandContext.Guild.Id);

                if (result)
                {
                    await commandContext.RespondAsync($"Registration complete! You now have {coreGame.RegistrationValue} vtuber cash!");
                    logger?.Log($"discord.RegisterCommand({commandContext.User.Id}, {commandContext.Guild.Id}) - true");
                }
                else
                {
                    logger?.Log($"discord.RegisterCommand({commandContext.User.Id}, {commandContext.Guild.Id}) - false");
                }

                logger?.Log($"discord.RegisterCommand({commandContext.Guild.Id}, {commandContext.Guild.Id}) - end");
            }
            catch (Exception ex)
            {
                await commandContext.RespondAsync(ex.Message);
            }
        }

        [Command("daily")]
        public async Task DailyCommand(CommandContext commandContext)
        {
            try
            {
                var coreGame = commandContext.Dependencies.GetDependency<IVTubeMonCoreGame>();
                var logger = commandContext.Dependencies.GetDependency<ILogger>();

                logger?.Log($"discord.DailyCommand({commandContext.User.Id}, {commandContext.Guild.Id}) - start");
                var dailyCheckinResult = coreGame.DailyCheckIn(commandContext.User.Id, commandContext.Guild.Id);

                if (dailyCheckinResult.CheckInSuccessfull)
                {
                    await commandContext.RespondAsync($"Daily check-in complete! You have gained {coreGame.DailyCheckinValue} vtuber cash!");
                    logger?.Log($"discord.DailyCommand({commandContext.User.Id}, {commandContext.Guild.Id}) - true");
                }
                else
                {
                    logger?.Log($"discord.DailyCommand({commandContext.User.Id}, {commandContext.Guild.Id}) - false");
                }

                logger?.Log($"discord.DailyCommand({commandContext.Guild.Id}, {commandContext.Guild.Id}) - end");
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
        public async Task RefreshCommand(CommandContext commandContext)
        {
            var dataCache = commandContext.Dependencies.GetDependency<DataCache>();
            var logger = commandContext.Dependencies.GetDependency<ILogger>();

            dataCache.RefreshAll();
            await commandContext.RespondAsync("Data Refreshed!");
        }

        [Command("ping")]
        public async Task PingCommand(CommandContext commandContext, DiscordMember member)
        {
            await commandContext.RespondAsync($"{member.Mention}! Someone wants you!");
        }

        [Command("select")]
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
