using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
    }
}
