using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTubeMon.Discord
{
    class VTubeMonDiscordHelpFormatter : BaseHelpFormatter
    {
        private DiscordEmbedBuilder embedBuilder { get; }

        public VTubeMonDiscordHelpFormatter(CommandContext ctx) : base(ctx)
        {
            this.embedBuilder = new DiscordEmbedBuilder()
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = "VTubeMon",
                    IconUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQvvasYj2sB0wJwTwGgkqrOEAVo_ZLDzcY_3A&usqp=CAU",
                    Url = "https://github.com/ajchance123/vtubemon",
                },
                Description = "Vtubemon is a collection game made for your discord server! Play minigames, collect vtubers and arm them up to fight" +
                " against other users' vtubers and gain rewards for your account.\n\nTo start type `v!register` to join the game and start collecting! Good luck!\n",
                Color = DiscordColor.Blurple,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    IconUrl = "https://iconsplace.com/wp-content/uploads/_icons/ffa500/256/png/help-icon-11-256.png",
                    Text = "Beep boop I'm a helpful tuber!"
                },
                Timestamp = DateTime.UtcNow,
            };
        }

        public override CommandHelpMessage Build()
        {
            return new CommandHelpMessage(embed: embedBuilder);
        }

        public override BaseHelpFormatter WithCommand(Command command)
        {
            this.embedBuilder.AddField(command.Name + $" ({String.Join(",", command.Aliases)})", command.Description, command is CommandGroup ? false : true);

            return this;
        }

        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        {
            StringBuilder commands = new StringBuilder();

            foreach (var command in subcommands)
            {
                commands.AppendLine($"{command.Name}{(command.Aliases.Count != 0 ? "("+ String.Join(",", command.Aliases)+")" : "")}: " +
                    $"{(command.Description != null ? command.Description : "No description")}");
            }

            this.embedBuilder.AddField("Commands", commands.ToString());

            return this;
        }
    }
}
