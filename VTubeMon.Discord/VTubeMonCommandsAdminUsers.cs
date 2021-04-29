using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using VTubeMon.API;
using DSharpPlus;
using VTubeMon.Game;

namespace VTubeMon.Discord
{
    [Group("guildadmin")]
    [Description("Administrative commands")]
    //Uncomment the code below to make the guild managers the only ones to call these commands
    [RequirePermissions(Permissions.ManageGuild)]
    [Hidden]
    public class VTubeMonCommandsAdminUsers : BaseCommandModule
    {
        // all the commands will need to be executed as <prefix>admin <command> <arguments>
        
        [Command("helloworld")]
        public async Task PingCommand(CommandContext commandContext)
        {
            await commandContext.RespondAsync($"Hello world!");
        }

        [Command("makeadmin")]
        [Description("Assign specified user admin role")]
        public async Task MakeAdmin(CommandContext commandContext, [Description("Specified user")] DiscordMember member)
        {
            try
            {
                var coreGame = commandContext.Services.GetService(typeof(IVTubeMonCoreGame)) as IVTubeMonCoreGame;
                var logger = commandContext.Services.GetService(typeof(ILogger)) as ILogger;

                logger?.Log($"discord.ToggleAdmin({member.Id}{commandContext.Guild.Id} - start");

                var result = coreGame.ToggleAdmin(commandContext.User.Id, member.Id, commandContext.Guild.Id, true);

                logger?.Log($"discord.ToggleAdmin({member.Id}{commandContext.Guild.Id} - {result.ResultType}");

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
                    case CommandResultType.NotExist:
                        await commandContext.RespondAsync($"{member.DisplayName} is not registered!");
                        break;
                }

                logger?.Log($"discord.RegisterCommand({commandContext.User.Id}{commandContext.Guild.Id} - end");
            }
            catch (Exception ex)
            {
                await commandContext.RespondAsync(ex.Message);
            }
        }

        [Command("removeadmin")]
        [Description("Removes specified user from admin permissions")]
        public async Task RemoveAdmin(CommandContext commandContext, [Description("Specified user")] DiscordMember member)
        {
            try
            {
                var coreGame = commandContext.Services.GetService(typeof(IVTubeMonCoreGame)) as IVTubeMonCoreGame;
                var logger = commandContext.Services.GetService(typeof(ILogger)) as ILogger;

                logger?.Log($"discord.ToggleAdmin({member.Id}{commandContext.Guild.Id} - start");

                var result = coreGame.ToggleAdmin(commandContext.User.Id, member.Id, commandContext.Guild.Id, false);

                logger?.Log($"discord.ToggleAdmin({member.Id}{commandContext.Guild.Id} - {result.ResultType}");

                switch (result.ResultType)
                {
                    case CommandResultType.Success:
                        await commandContext.RespondAsync($"{member.DisplayName} has been removed from admins!");
                        break;
                    case CommandResultType.Failure:
                        await commandContext.RespondAsync($"Sorry an error has occured {result.Error}, go yell at the devs");
                        break;
                    case CommandResultType.Duplicate:
                        await commandContext.RespondAsync($"{member.DisplayName} isn't an admin!");
                        break;
                    case CommandResultType.NotExist:
                        await commandContext.RespondAsync($"{member.DisplayName} is not registered!");
                        break;
                        /*case CommandResultType.Unauthorized:
                            await commandContext.RespondAsync($"You are not authorized to do this.");
                            break;*/
                }

                logger?.Log($"discord.RegisterCommand({commandContext.User.Id}{commandContext.Guild.Id} - end");
            }
            catch (Exception ex)
            {
                await commandContext.RespondAsync(ex.Message);
            }
        }
    }
}
