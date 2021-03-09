using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using VTubeMon.Data;

namespace VTubeMon.Discord
{
    public class VTubeMonCommands
    {
        [Command("list")]
        public async Task ListCommand(CommandContext commandContext)
        {
            var dataCache = commandContext.Dependencies.GetDependency<DataCache>();
            foreach(var vtuber in dataCache.VtuberCache.CachedList)
            {
                await commandContext.RespondAsync(vtuber.EnName.Value);
            }
        }
    }
}
