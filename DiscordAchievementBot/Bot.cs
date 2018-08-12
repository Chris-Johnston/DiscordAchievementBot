using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordAchievementBot
{
    public class Bot : IDisposable
    {
        private DiscordSocketClient client;
        
        public async Task Start()
        {
            // define DiscordSocketClient
            client = new DiscordSocketClient(new DiscordSocketConfig() { LogLevel = Discord.LogSeverity.Info });

            // log in
            await client.LoginAsync(Discord.TokenType.Bot, Program.GlobalConfig.Data.ConnectionToken).ConfigureAwait(false);
            await client.StartAsync().ConfigureAwait(false);

            var handler = new CommandHandler();

            // set up a service collection for Dependency Injection
            var serviceProvider = new ServiceCollection()
                .AddSingleton(client)
                .AddSingleton(handler)
                .AddSingleton(new GamerScoreGenerator())
                .AddSingleton(new ImageGenerator(Program.GlobalConfig.Data))
                .BuildServiceProvider();

            await handler.Install(client, serviceProvider).ConfigureAwait(false);

            client.Log += LogAsync;
            client.Ready += Client_Ready;
            client.GuildAvailable += Client_GuildAvailable;

            await client.SetGameAsync($"Type: {GlobalConfiguration.CommandPrefix}Help").ConfigureAwait(false);

            await Task.Delay(-1).ConfigureAwait(false);
        }

        private async Task Client_GuildAvailable(SocketGuild arg)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"Connected to guild {arg.Name} with {arg.MemberCount} members. ID {arg.Id}");
            }).ConfigureAwait(false);
        }

        private async Task Client_Ready()
        {
            Console.WriteLine($"Part of {client.Guilds.Count} guilds.");

            var application = await client.GetApplicationInfoAsync().ConfigureAwait(false);
            Log(new LogMessage(LogSeverity.Info, "Program",
                $"Invite URL: <https://discordapp.com/oauth2/authorize?client_id={application.Id}&scope=bot>"));
            Console.WriteLine($"Invite URL: <https://discordapp.com/oauth2/authorize?client_id={application.Id}&scope=bot>");
        }

        public static void Log(Discord.LogMessage arg)
        {
            Console.WriteLine(arg.ToString());
        }

        public static async Task LogAsync(LogMessage message)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(message);
            }).ConfigureAwait(false);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                client?.Dispose();
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
