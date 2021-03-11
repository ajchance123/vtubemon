using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using System;
using System.IO;
using System.Threading.Tasks;
using VTubeMon.API;
using VTubeMon.API.Core;
using VTubeMon.Data;

namespace VTubeMon.Discord
{
    public class VTubeMonDiscord : IVTubeMonServerConnection
    {
        public VTubeMonDiscord(DataCache dataCache, IVTubeMonDbConnection vTubeMonDbConnection, IVTubeMonCoreGame vTubeMonCoreGame, ILogger logger)
        {
            _dataCache = dataCache;
            _vTubeMonDbConnection = vTubeMonDbConnection;
            _vTubeMonCoreGame = vTubeMonCoreGame;
            _logger = logger;
        }

        private DiscordClient _client;

        private const string TokenFile = "DiscordToken.txt";
        private DataCache _dataCache;
        private IVTubeMonDbConnection _vTubeMonDbConnection;
        private IVTubeMonCoreGame _vTubeMonCoreGame;
        private ILogger _logger;

        public void CreateNewClient(string prefix = "v!")
        {
            if (!File.Exists(TokenFile))
            {
                throw new Exception("Token File Missing");
            }

            var token = File.ReadAllLines(TokenFile)[0];

            _client = new DiscordClient(new DiscordConfiguration()
            {
                Token = token,
                TokenType = TokenType.Bot
            });

            var dependencyCollectionBuilder = new DependencyCollectionBuilder();
            dependencyCollectionBuilder.AddInstance(_dataCache);
            dependencyCollectionBuilder.AddInstance(_vTubeMonDbConnection);
            dependencyCollectionBuilder.AddInstance(_vTubeMonCoreGame);
            dependencyCollectionBuilder.AddInstance(_logger);

            var interactivity = _client.UseInteractivity(new InteractivityConfiguration());
            dependencyCollectionBuilder.AddInstance(interactivity);

            var commandModule = _client.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefix = prefix,
                Dependencies = dependencyCollectionBuilder.Build()

            });

            commandModule.RegisterCommands<VTubeMonCommands>();
        }

        public int Ping { get; }

        public event EventHandler<bool> OnConnect;
        public async Task ConnectAsync()
        {
            try
            {
                await _client.ConnectAsync();
                OnConnect?.Invoke(this, true);
            }
            catch (Exception ex)
            {
                _logger?.Log(ex);
                OnConnect?.Invoke(this, false);
                throw ex;
            }
        }

        public event EventHandler<bool> OnDisconnect;
        public async Task DisconnectAsync()
        {
            try
            {
                await _client.DisconnectAsync();
                OnDisconnect?.Invoke(this, true);
            }
            catch (Exception ex)
            {
                _logger?.Log(ex);
                OnDisconnect?.Invoke(this, false);
                throw ex;
            }
        }
    }
}
