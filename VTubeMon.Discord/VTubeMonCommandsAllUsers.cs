using DSharpPlus;
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
    public class VTubeMonCommandsAllUsers : BaseCommandModule
    {
        [Command("list")]
        [Description("Prints out vtuber list")]
        public async Task ListCommand(CommandContext commandContext)
        {
            var dataCache = commandContext.Services.GetService(typeof(DataCache)) as DataCache;
            var logger = commandContext.Services.GetService(typeof(ILogger)) as ILogger;

            logger?.Log($"discord.ListCommand({commandContext.Guild.Id}) - start");

            await commandContext.RespondAsync(string.Join(Environment.NewLine, dataCache.VtuberCache.CachedList.Select(v=>v.EnName.Value)));

            logger?.Log($"discord.ListCommand({commandContext.Guild.Id}) - end");
        }

        [Command("register")]
        [Description("Registers user")]
        public async Task RegisterCommand(CommandContext commandContext)
        {
            try
            {
                var coreGame = commandContext.Services.GetService(typeof(IVTubeMonCoreGame)) as IVTubeMonCoreGame;
                var logger = commandContext.Services.GetService(typeof(ILogger)) as ILogger;

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
        [Description("Daily check-in")]
        public async Task DailyCommand(CommandContext commandContext)
        {
            try
            {
                var coreGame = commandContext.Services.GetService(typeof(IVTubeMonCoreGame)) as IVTubeMonCoreGame;
                var logger = commandContext.Services.GetService(typeof(ILogger)) as ILogger;

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
                    case CommandResultType.NotExist:
                        await commandContext.RespondAsync($"You are not registered!");
                        break;
                }

                logger?.Log($"discord.DailyCommand({commandContext.User.Id}{commandContext.Guild.Id} - end");
            }
            catch (Exception ex)
            {
                await commandContext.RespondAsync(ex.Message);
            }
        }

        [Command("cash")]
        [Description("Get's the sum value of your wallet")]
        public async Task CashCommand(CommandContext commandContext)
        {
            var logger = commandContext.Services.GetService(typeof(ILogger)) as ILogger;
            var coreGame = commandContext.Services.GetService(typeof(IVTubeMonCoreGame)) as IVTubeMonCoreGame;

            logger?.Log($"discord.CashCommand({commandContext.Guild.Id}) - start");

            int result = coreGame.TotalCash(commandContext.User.Id, commandContext.Guild.Id);
            await commandContext.RespondAsync($"You have {result} vTuber cash!");

            logger?.Log($"discord.CashCommand({commandContext.Guild.Id}) - end");
        }
    }
}
