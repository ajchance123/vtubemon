using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTubeMon.API;
using VTubeMon.Data;
using VTubeMon.Data.Commands;

namespace VTubeMon.Discord
{
    [Group("dev")]
    [Description("Developer Commands")]
    [Hidden]
    class VTubeMonCommandsDev : BaseCommandModule
    {
        HashSet<ulong> DevIDs = new HashSet<ulong>()
        {
            170419106451816449, //Annick
            341737465209552899, //Austin
            135628611133636608, //Jason
            109448959474294784, //Joey
            209805653835776000, //Jordan
        };

        [Command("refresh")]
        public async Task RefreshCommand(CommandContext commandContext)
        {
            if (!DevIDs.Contains(commandContext.Member.Id))
                return;

            var dataCache = commandContext.Services.GetService(typeof(DataCache)) as DataCache;
            var logger = commandContext.Services.GetService(typeof(ILogger)) as ILogger;

            dataCache.RefreshAll();
            await commandContext.RespondAsync("Data Refreshed!");
        }

        [Command("ping")]
        public async Task PingCommand(CommandContext commandContext, DiscordMember member)
        {
            if (!DevIDs.Contains(commandContext.Member.Id))
                return;

            await commandContext.RespondAsync($"{member.Mention}! Someone wants you!");
        }

        [Command("select")]
        public async Task SelectCommand(CommandContext commandContext)
        {
            if (!DevIDs.Contains(commandContext.Member.Id))
                return;

            try
            {
                var dbConnection = commandContext.Services.GetService(typeof(IVTubeMonDbConnection)) as IVTubeMonDbConnection;
                var logger = commandContext.Services.GetService(typeof(ILogger)) as ILogger;

                var command = new MultiStringSelectCommand($"SELECT {commandContext.RawArgumentString}");

                var discordMessage = await commandContext.RespondAsync(string.Join(Environment.NewLine, dbConnection.ExecuteDbQueryCommand(command).Select((s) => string.Join("\t", s))));
            }
            catch (Exception ex)
            {
                await commandContext.RespondAsync(ex.Message);
            }
        }

        [Command("nonQuery")]
        public async Task NonQueryCommand(CommandContext commandContext)
        {
            if (!DevIDs.Contains(commandContext.Member.Id))
                return;

            try
            {
                var dbConnection = commandContext.Services.GetService(typeof(IVTubeMonDbConnection)) as IVTubeMonDbConnection;

                //var rows = dbConnection.ExecuteDbNonQueryCommand(new )

                //await commandContext.RespondAsync($"{rows} row(s) affected");
            }
            catch (Exception ex)
            {
                await commandContext.RespondAsync(ex.Message);
            }
        }
    }
}
