using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTubeMon.Discord
{
    class VTubeMonDiscordHelpFormatter : BaseHelpFormatter
    {
        private DiscordEmbedBuilder EmbedBuilder { get; }
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
                Color = DiscordColor.Gold,
            };
        }

        public override CommandHelpMessage Build()
        {
            return new CommandHelpMessage("", EmbedBuilder.Build());
        }

        public override BaseHelpFormatter WithCommand(Command command)
        {
            if (command is CommandGroup)
                this.EmbedBuilder.AddField(command.Name, command.Description, false);
            else
                this.EmbedBuilder.AddField(command.Name, command.Description, true);

            return this;
        }

        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        {
            return this;
        }
    }
}
