using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VTubeMon.Core
{
    public class VTubeMonCommands
    {
        [Command("Greet")]
        public async Task GreetCommand(CommandContext commandContext)
        {
            await commandContext.RespondAsync("yeah");
        }
    }
}
