using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;

namespace VTubeMon.Discord
{
    class VTubeMonDiscordHelpFormatter : BaseHelpFormatter
    {
        public DiscordEmbedBuilder EmbedBuilder { get; }
        private Command Command { get; set; }

        public VTubeMonDiscordHelpFormatter(CommandContext ctx) : base(ctx)
        {
            this.EmbedBuilder = new DiscordEmbedBuilder()
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = "HelpVTuber"
                },
                Title = "Help Menu",
                Color = DiscordColor.Gold
            };
        }

        public override CommandHelpMessage Build()
        {
            throw new NotImplementedException();
        }

        public override BaseHelpFormatter WithCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        {
            throw new NotImplementedException();
        }
    }
}
