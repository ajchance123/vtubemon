﻿using DSharpPlus.Entities;
using System.Collections.Generic;
using System.Linq;
using VTubeMon.API.Server;

namespace VTubeMon.Discord
{
    public class VTubeMonDiscordServer : IServer
    {
        public VTubeMonDiscordServer(DiscordGuild discordGuild)
        {
            _discordGuild = discordGuild;
        }

        private DiscordGuild _discordGuild;

        public ulong Id => _discordGuild.Id;

        public string Name => _discordGuild.Name;


        public async void SendMessageToDefaultChannel(string message)
        {
            var defaultChannel = _discordGuild.GetDefaultChannel();
            await defaultChannel.SendMessageAsync(message);
        }

        public async void SendMessageToDefaultChannel(string message, string fileName)
        {
            var defaultChannel = _discordGuild.GetDefaultChannel();
            await defaultChannel.SendFileAsync(fileName, message);
        }

        public IEnumerable<IChannel> Channels => _discordGuild.Channels.Where(c => c.Type == DSharpPlus.ChannelType.Text).Select(c => { IChannel channel = new VTubeMonChannel(c); return channel; });
    }
}