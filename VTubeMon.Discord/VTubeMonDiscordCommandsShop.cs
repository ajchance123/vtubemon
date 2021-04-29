using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using static DSharpPlus.Entities.DiscordEmbedBuilder;

namespace VTubeMon.Discord
{
    class VTubeMonDiscordCommandsShop
    {
        public DiscordEmbedBuilder EmbedBuilder { get; }
        private Command Command { get; set; }

        public VTubeMonDiscordCommandsShop(CommandContext ctx)
        {
            this.EmbedBuilder = new DiscordEmbedBuilder()
            {
                Author = new EmbedAuthor()
                {
                    Name = "Shop Keeper"
                },
                Color = DiscordColor.Blurple,
                Title = "VTuber Shop"
            };
        }
    }
}
