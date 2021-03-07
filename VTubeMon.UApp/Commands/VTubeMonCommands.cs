using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

public class VTubeMonCommands
{
    [Command("catch")]
    public async Task CatchCommand(CommandContext ctx) 
    {
        await ctx.RespondAsync("Greetings! Thank you for executing me!");
    }
}
