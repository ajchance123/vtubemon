using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using VTubeMon.Data;

namespace VTubeMon.Core
{
    public class VTubeMonCommands
    {
        [Command("ListVTubers")]
        public async Task ListVTubersCommand(CommandContext commandContext)
        {
            var dataCache = commandContext.Dependencies.GetDependency<DataCache>();
            foreach(var vtuber in dataCache.VtuberCache.CachedList)
            {
                await commandContext.RespondAsync(vtuber.EnName);
            }
        }

        //Admin Commands
        [Command("RefrehData")]
        public async Task RefreshDataCommand(CommandContext commandContext)
        {
            var dataCache = commandContext.Dependencies.GetDependency<DataCache>();
            dataCache.RefreshAll();
            await commandContext.RespondAsync("Data Refreshed!");
        }
    }
}
