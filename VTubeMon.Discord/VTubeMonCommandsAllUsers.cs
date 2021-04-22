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
    public class VTubeMonCommandsAllUsers
    {
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
        [Description("Initializes user's daily check in")]
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
    }
}
