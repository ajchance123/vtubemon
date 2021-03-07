using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.IO;
using VTubeMon.Data;

namespace VTubeMon.Core
{
    public class VTubeMonDiscord
    {
        public VTubeMonDiscord(DataCache dataCache)
        {
            _dataCache = dataCache;
        }

        public DiscordClient Client { get; private set; }

        private const string TokenFile = "DiscordToken.txt";
        private DataCache _dataCache;

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

            var dependencyCollectionBuilder = new DependencyCollectionBuilder();
            dependencyCollectionBuilder.AddInstance(_dataCache);
            var commandModule = Client.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefix = "v!",
                Dependencies = dependencyCollectionBuilder.Build()
                    
            });

            commandModule.RegisterCommands<VTubeMonCommands>();

            return Client;
        }
    }
}
