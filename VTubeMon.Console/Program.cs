using DSharpPlus;
using System;
using System.Threading.Tasks;

namespace VTubeMon.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("VTubers rock");
        }

        public static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = "ODE3MTQ3NTIwNzM1NjQxNjQw.YEFR7g.7xdD_sWWLUoD3EmlLzhdFX61KSQ",
                TokenType = TokenType.Bot
            });

            await discord.ConnectAsync();
        }
    }
}
