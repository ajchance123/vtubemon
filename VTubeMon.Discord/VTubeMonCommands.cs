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

        [Command("list")]
        public async Task ListCommand(CommandContext commandContext)
        {
            var dataCache = commandContext.Dependencies.GetDependency<DataCache>();
            foreach(var vtuber in dataCache.VtuberCache.CachedList)
            {
                await commandContext.RespondAsync(vtuber.EnName.Value);
            }
        }

        #endregion


        #region SUPER COOL ADMIN COMMANDS - NO COMMON FOLK ALLOWED

        //these commands should check the user id before we actually exeute them

        [Command("refresh")]
        public async Task RefreshCommand(CommandContext commandContext)
        {
            var dataCache = commandContext.Dependencies.GetDependency<DataCache>();
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

                var command = new MultiStringSelectCommand($"SELECT {commandContext.RawArgumentString}");

                var discordMessage = await commandContext.RespondAsync(string.Join(Environment.NewLine, dbConnection.ExecuteDbSelectCommand(command).Select((s) => string.Join("\t", s))));

                var reaction = await interactivity.WaitForMessageReactionAsync(discordMessage);

                var emoji = reaction.Emoji;
            }
            catch(Exception ex)
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

                var rows = dbConnection.ExecuteNonQuery(commandContext.RawArgumentString);

                await commandContext.RespondAsync($"{rows} row(s) affected");
            }
            catch (Exception ex)
            {
                await commandContext.RespondAsync(ex.Message);
            }
        }

        #endregion
    }
}
