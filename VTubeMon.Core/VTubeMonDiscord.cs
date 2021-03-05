using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.IO;

namespace VTubeMon.Core
{
    public class VTubeMonDiscord
    {
        public DiscordClient Client { get; private set; }

        private const string TokenFile = "DiscordToken.txt";
        public VTubeMonDiscord()
        {
        }

        public DiscordClient CreateNewClient()
        {
            if (!File.Exists(TokenFile))
            {
                throw new Exception("Token File Missing");
            }

            var token = File.ReadAllLines(TokenFile)[0];

            Client = new DiscordClient(new DiscordConfiguration()
            {
                Token = token,
                TokenType = TokenType.Bot
            });

            var commands = Client.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefix = "!v-"
            });

            commands.RegisterCommands<VTubeMonCommands>();

            return Client;
        }
    }
}
