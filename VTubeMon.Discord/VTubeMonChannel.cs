using DSharpPlus.Entities;
using VTubeMon.API;

namespace VTubeMon.Discord
{
    public class VTubeMonChannel : IChannel
    {
        public VTubeMonChannel(DiscordChannel discordChannel)
        {
            _discordChannel = discordChannel;
        }
        
        private DiscordChannel _discordChannel;

        public string Name => _discordChannel.Name;

        public async void SendMessage(string message)
        {
            await _discordChannel.SendMessageAsync(message);
        }

        public async void SendMessage(string message, string fileName)
        {
            await _discordChannel.SendMessageAsync(embed: new DiscordEmbedBuilder
            {
                ImageUrl = fileName
            });
        }

        public async void SendMessage(DiscordEmbed de)
        {
            await _discordChannel.SendMessageAsync(de);
        }
    }
}
