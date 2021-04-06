﻿using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using VTubeMon.API.Server;

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
            await _discordChannel.SendFileAsync(fileName, message);
        }
    }
}
