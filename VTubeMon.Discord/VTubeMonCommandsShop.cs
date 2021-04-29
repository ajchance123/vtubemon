using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Threading.Tasks;
using VTubeMon.API;
using static DSharpPlus.Entities.DiscordEmbedBuilder;

namespace VTubeMon.Discord
{
    [Group("Shop"), Aliases("s")]
    [Description("Summons the shop")]
    class VTubeMonCommandsShop : BaseCommandModule
    {
        public DiscordEmbedBuilder EmbedBuilder { get; }

        public VTubeMonCommandsShop()
        {
            this.EmbedBuilder = new DiscordEmbedBuilder()
            {
                Author = new EmbedAuthor()
                {
                    IconUrl = "https://ih1.redbubble.net/image.1827059313.9135/flat,128x128,075,t-pad,128x128,f8f8f8.jpg",
                    Name = "Shop Keeper"
                },
                Color = DiscordColor.Gold,
                Footer = new EmbedFooter()
                {
                    Text = "Thank you for browsing the store",
                    IconUrl = "https://www.iconbunny.com/icons/media/catalog/product/1/0/1087.12-shopping-bag-icon-iconbunny.jpg"
                },
                Timestamp = DateTime.UtcNow
            };
        }

        [GroupCommand]
        public async Task shopCommand(CommandContext commandContext)
        {
            var logger = commandContext.Services.GetService(typeof(ILogger)) as ILogger;
            var coreGame = commandContext.Services.GetService(typeof(IVTubeMonCoreGame)) as IVTubeMonCoreGame;
            var interactivity = commandContext.Client.GetInteractivity();

            logger?.Log($"discord.shopCommand({commandContext.Guild.Id}) - start");

            createStoreFront();
            var message = await commandContext.RespondAsync(embed: EmbedBuilder);

            var swordEmoji = DiscordEmoji.FromName(commandContext.Client, ":crossed_swords:");
            var buffEmoji = DiscordEmoji.FromName(commandContext.Client, ":shield:");
            var flairEmoji = DiscordEmoji.FromName(commandContext.Client, ":star:");

            await message.CreateReactionAsync(swordEmoji);
            await message.CreateReactionAsync(buffEmoji);
            await message.CreateReactionAsync(flairEmoji);

            var result = await message.WaitForReactionAsync(commandContext.Member);

            if (result.TimedOut)
                return;

            if(result.Result.Emoji.Equals(swordEmoji))
            {
                int userCash = coreGame.TotalCash(commandContext.User.Id, commandContext.Guild.Id);
                createArmory(userCash);
                await commandContext.RespondAsync(embed: EmbedBuilder);
            }

            logger?.Log($"discord.shopCommand({commandContext.Guild.Id}) - end");
        }

        [Command("buy"), Aliases("b")]
        [Description("Allows you to purchase items from the store")]
        public async Task PingCommand(CommandContext commandContext, [Description("Item you wish to purchase")]String item)
        {
            await commandContext.RespondAsync("Thank you for your purchase");
        }

        private void createStoreFront()
        {
            this.EmbedBuilder.ClearFields();

            this.EmbedBuilder.WithDescription("Welcome to the shop! Here you can buy armor and buffs for your vtubers, purchase account modifiers to strut" +
                " your stuff, and buy various knicknacks to show off to others on the server! Please select what catagory to browse with `v!s [catagory]`," +
                " or you may select a reaction below.");
            this.EmbedBuilder.AddField("Armory :crossed_swords:", "Purchase individual armor pieces or sets of armor to increase your vtuber stats");
            this.EmbedBuilder.AddField("Buffs :shield:", "Enhance your vtuber with buffs that can last for a battle or a period of time");
            this.EmbedBuilder.AddField("Flair :star:", "Make your own account extravagent with these flairs");
        }

        private void createArmory(int cash)
        {
            this.EmbedBuilder.ClearFields();

            this.EmbedBuilder.WithDescription("Welcome to the armory! Please peruse the merchandise and let me know what you would like to buy.");
            this.EmbedBuilder.AddField("Sets", "lots of emptiness");
            this.EmbedBuilder.AddField("Armor Pieces", "more emptiness");
            this.EmbedBuilder.Footer.Text = $"User's total cash: {cash}";
        }
    }
}
