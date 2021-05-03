using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Threading.Tasks;
using VTubeMon.API;
using static DSharpPlus.Entities.DiscordEmbedBuilder;

namespace VTubeMon.Discord
{
    enum ShopPage
    {
        StoreFront,
        Armory,
        Buffs,
        Flair,
        TimedOut
    }

    [Group("Shop"), Aliases("s")]
    [Description("Summons the shop")]
    class VTubeMonCommandsShop : BaseCommandModule
    {
        ShopPage page = ShopPage.StoreFront;

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
            page = ShopPage.StoreFront;
            await shopLoop(commandContext);
        }

        [Command("armory")]
        [Description("Takes you to the store's armory page")]
        public async Task ArmoryCommand(CommandContext commandContext)
        {
            page = ShopPage.Armory;
            await shopLoop(commandContext);
        }

        [Command("buffs")]
        [Description("Takes you to the store's buffs page")]
        public async Task BuffsCommand(CommandContext commandContext)
        {
            page = ShopPage.Buffs;
            await shopLoop(commandContext);
        }

        [Command("flair")]
        [Description("Takes you to the store's flair page")]
        public async Task FlairCommand(CommandContext commandContext)
        {
            page = ShopPage.Flair;
            await shopLoop(commandContext);
        }

        [Command("buy"), Aliases("b")]
        [Description("Allows you to purchase items from the store")]
        public async Task BuyCommand(CommandContext commandContext, [Description("Item you wish to purchase")]String item)
        {
            var logger = commandContext.Services.GetService(typeof(ILogger)) as ILogger;
            logger?.Log($"discord.buyCommand({commandContext.Guild.Id}) - start");

            //do stuff with store inventory and player inventory

            await commandContext.RespondAsync("Thank you for your purchase");

            logger?.Log($"discord.buyCommand({commandContext.Guild.Id}) - end");
        }

        private async Task shopLoop(CommandContext commandContext)
        {
            var logger = commandContext.Services.GetService(typeof(ILogger)) as ILogger;
            var coreGame = commandContext.Services.GetService(typeof(IVTubeMonCoreGame)) as IVTubeMonCoreGame;
            int playerValue = coreGame.TotalCash(commandContext.User.Id, commandContext.Guild.Id);
            bool shopping = true;

            logger?.Log($"discord.shopCommand({commandContext.Guild.Id}) - start");

            while (shopping)
            {
                switch (page)
                {
                    case ShopPage.StoreFront:
                        await storeFront(commandContext);
                        break;
                    case ShopPage.Armory:
                        await armoryFront(commandContext, playerValue);
                        break;
                    case ShopPage.Buffs:
                        await buffsFront(commandContext, playerValue);
                        break;
                    case ShopPage.Flair:
                        await flairsFront(commandContext, playerValue);
                        break;
                    case ShopPage.TimedOut:
                        shopping = false;
                        break;
                }
                playerValue = coreGame.TotalCash(commandContext.User.Id, commandContext.Guild.Id);
            }

            logger?.Log($"discord.shopCommand({commandContext.Guild.Id}) - end");
        }

        private async Task storeFront(CommandContext commandContext)
        {
            createStoreFront();
            var message = await commandContext.RespondAsync(embed: EmbedBuilder);

            await message.CreateReactionAsync(DiscordEmoji.FromName(commandContext.Client, ":crossed_swords:"));
            await message.CreateReactionAsync(DiscordEmoji.FromName(commandContext.Client, ":shield:"));
            await message.CreateReactionAsync(DiscordEmoji.FromName(commandContext.Client, ":star:"));

            var result = await message.WaitForReactionAsync(commandContext.Member);

            setPage(result);

            if (page == ShopPage.TimedOut)
                return;

            await message.DeleteAsync();
        }

        private async Task armoryFront(CommandContext commandContext, int playerValue)
        {
            createArmory(playerValue, commandContext.User.Username);
            var message = await commandContext.RespondAsync(embed: EmbedBuilder);

            await message.CreateReactionAsync(DiscordEmoji.FromName(commandContext.Client, ":moneybag:"));
            await message.CreateReactionAsync(DiscordEmoji.FromName(commandContext.Client, ":shield:"));
            await message.CreateReactionAsync(DiscordEmoji.FromName(commandContext.Client, ":star:"));

            var result = await message.WaitForReactionAsync(commandContext.Member);

            setPage(result);

            if (page == ShopPage.TimedOut)
                return;

            await message.DeleteAsync();
        }

        private async Task buffsFront(CommandContext commandContext, int playerValue)
        {
            createBuffs(playerValue, commandContext.User.Username);
            var message = await commandContext.RespondAsync(embed: EmbedBuilder);

            await message.CreateReactionAsync(DiscordEmoji.FromName(commandContext.Client, ":moneybag:"));
            await message.CreateReactionAsync(DiscordEmoji.FromName(commandContext.Client, ":crossed_swords:"));
            await message.CreateReactionAsync(DiscordEmoji.FromName(commandContext.Client, ":star:"));

            var result = await message.WaitForReactionAsync(commandContext.Member);

            setPage(result);

            if (page == ShopPage.TimedOut)
                return;

            await message.DeleteAsync();
        }

        private async Task flairsFront(CommandContext commandContext, int playerValue)
        {
            createFlairs(playerValue, commandContext.User.Username);
            var message = await commandContext.RespondAsync(embed: EmbedBuilder);

            await message.CreateReactionAsync(DiscordEmoji.FromName(commandContext.Client, ":moneybag:"));
            await message.CreateReactionAsync(DiscordEmoji.FromName(commandContext.Client, ":crossed_swords:"));
            await message.CreateReactionAsync(DiscordEmoji.FromName(commandContext.Client, ":shield:"));

            var result = await message.WaitForReactionAsync(commandContext.Member);

            setPage(result);

            if (page == ShopPage.TimedOut)
                return;

            await message.DeleteAsync();
        }

        private void setPage(InteractivityResult<DSharpPlus.EventArgs.MessageReactionAddEventArgs> messageResult)
        {
            if (messageResult.TimedOut)
            {
                page = ShopPage.TimedOut;
                return;
            }

            switch (messageResult.Result.Emoji.GetDiscordName())
            {
                case ":crossed_swords:":
                    page = ShopPage.Armory;
                    break;
                case ":shield:":
                    page = ShopPage.Buffs;
                    break;
                case ":star:":
                    page = ShopPage.Flair;
                    break;
                case ":moneybag:":
                    page = ShopPage.StoreFront;
                    break;
            }
        }

        private void createStoreFront()
        {
            this.EmbedBuilder.ClearFields();

            this.EmbedBuilder.WithDescription("Welcome to the shop! Here you can buy armor and buffs for your vtubers, purchase account modifiers to strut" +
                " your stuff, and buy various knicknacks to show off to others on thes server! Please select what catagory to browse with `v!s [catagory]`," +
                " or you may select a reaction below.");
            this.EmbedBuilder.AddField("Armory :crossed_swords:", "Purchase individual armor pieces or sets of armor to increase your vtuber stats");
            this.EmbedBuilder.AddField("Buffs :shield:", "Enhance your vtuber with buffs that can last for a battle or a period of time");
            this.EmbedBuilder.AddField("Flair :star:", "Make your own account extravagent with these flairs");
        }

        private void createArmory(int cash, string name)
        {
            this.EmbedBuilder.ClearFields();

            this.EmbedBuilder.WithDescription("Welcome to the armory! Please peruse the merchandise and let me know what you would like to buy.");
            this.EmbedBuilder.AddField("Sets", "lots of emptiness");
            this.EmbedBuilder.AddField("Armor Pieces", "more emptiness");
            this.EmbedBuilder.Footer.Text = $"{name}'s total cash: {cash}";
        }

        private void createBuffs(int cash, string name)
        {
            this.EmbedBuilder.ClearFields();

            this.EmbedBuilder.WithDescription("Welcome to the buff store! Please peruse the merchandise and let me know what you would like to buy.");
            this.EmbedBuilder.AddField("Strength Buffs", "lots of emptiness");
            this.EmbedBuilder.AddField("Beauty Buffs", "more emptiness");
            this.EmbedBuilder.Footer.Text = $"{name}'s total cash: {cash}";
        }

        private void createFlairs(int cash, string name)
        {
            this.EmbedBuilder.ClearFields();

            this.EmbedBuilder.WithDescription("Welcome to the flair store! Please peruse the merchandise and let me know what you would like to buy.");
            this.EmbedBuilder.AddField("Profile Icons", "lots of emptiness");
            this.EmbedBuilder.AddField("Profile Titles", "more emptiness");
            this.EmbedBuilder.Footer.Text = $"{name}'s total cash: {cash}";
        }
    }
}
