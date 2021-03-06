﻿using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VTubeMon.API;
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
            _logger.Log($"VTubeMonDiscord.CreateNewClient({prefix}) - start");
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

            _client.Ready += On_Client_Ready;
            _client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(5)
            }); ;

            var serviceCollectionBuilder = new ServiceCollection();
            serviceCollectionBuilder.AddSingleton(_dataCache);
            serviceCollectionBuilder.AddSingleton(_vTubeMonDbConnection);
            serviceCollectionBuilder.AddSingleton(_vTubeMonCoreGame);
            serviceCollectionBuilder.AddSingleton(_logger);

            var commandModule = _client.UseCommandsNext(new CommandsNextConfiguration
            {
                CaseSensitive = false,
                StringPrefixes = new[] { prefix },
                Services = serviceCollectionBuilder.BuildServiceProvider()
            });
            
            commandModule.RegisterCommands<VTubeMonCommandsGeneral>();
            commandModule.RegisterCommands<VTubeMonCommandsAdmin>();
            commandModule.RegisterCommands<VTubeMonCommandsDev>();
            commandModule.RegisterCommands<VTubeMonCommandsShop>();
            commandModule.SetHelpFormatter<VTubeMonDiscordHelpFormatter>();

            _logger.Log($"VTubeMonDiscord.CreateNewClient({prefix}) - end");
        }

        public event EventHandler<bool> OnReadyChanged;
        private async Task On_Client_Ready(DiscordClient sender, ReadyEventArgs e)
        {
            OnReadyChanged?.Invoke(this, true);
        }

        public int Ping { get; }

        public event EventHandler<bool> OnConnect;
        public async Task ConnectAsync()
        {
            _logger.Log($"VTubeMonDiscord.ConnectAsync() - start");
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

            _logger.Log($"VTubeMonDiscord.ConnectAsync() - end");
        }

        public event EventHandler<bool> OnDisconnect;
        public async Task DisconnectAsync()
        {
            _logger.Log($"VTubeMonDiscord.Disconnect() - start");
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
            _logger.Log($"VTubeMonDiscord.Disconnect() - end");
        }

        public IEnumerable<IServer> Servers
        {
            get
            {
                return _client.Guilds.Select(g =>
                {
                    IServer server = new VTubeMonDiscordServer(g.Value);
                    return server;
                });
            }
        }
    }
}
