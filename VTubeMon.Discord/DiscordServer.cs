using DSharpPlus;
using DSharpPlus.Entities;
using VTubeMon.API.Server;

namespace VTubeMon.Discord
{
    public class DiscordServer : IServer
    {
        public DiscordServer(DiscordClient client, DiscordGuild discordGuild)
        {
            _discordGuild = discordGuild;
            _client = client;
        }

        private DiscordGuild _discordGuild;
        private DiscordClient _client;

        public ulong Id => _discordGuild.Id;

        public string Name => _discordGuild.Name;


        public void SendMessage(string message)
        {
            var defaultChannel = _discordGuild.GetDefaultChannel();
            _client?.SendMessageAsync(defaultChannel, message);
        }
    }
}
