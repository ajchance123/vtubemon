using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using System;
using System.IO;
using VTubeMon.API;
using VTubeMon.Data;

namespace VTubeMon.Discord
{
    public class VTubeMonDiscord
    {
        public VTubeMonDiscord(DataCache dataCache, IVTubeMonDbConnection vTubeMonDbConnection)
        {
            _dataCache = dataCache;
            _vTubeMonDbConnection = vTubeMonDbConnection;
        }

        public DiscordClient Client { get; private set; }

        private const string TokenFile = "DiscordToken.txt";
        private DataCache _dataCache;
        private IVTubeMonDbConnection _vTubeMonDbConnection;

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
            dependencyCollectionBuilder.AddInstance(_vTubeMonDbConnection);

            var interactivity = Client.UseInteractivity(new InteractivityConfiguration());
            dependencyCollectionBuilder.AddInstance(interactivity);

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
