using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;
using VTubeMon.API;
using VTubeMon.Data;
using VTubeMon.Data.Commands;

namespace VTubeMon.Core
{
    public class VTubeMonAdminCommands
    {
        //Admin Commands
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
        public async Task SelectCommand(CommandContext commandContext, string table, string column)
        {
            var dbConnection = commandContext.Dependencies.GetDependency<IVTubeMonDbConnection>();

            var command = new StringSelectCommand(table, column);

            await commandContext.RespondAsync(string.Join(Environment.NewLine, dbConnection.ExecuteDbSelectCommand(command)));
        }
    }
}
