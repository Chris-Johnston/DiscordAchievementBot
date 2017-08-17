﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordAchievementBot
{
    public class Bot
    {
        private DiscordSocketClient client;
        private CommandHandler handler;

        public async Task Start()
        {
            // define DiscordSocketClient

            client = new DiscordSocketClient(new DiscordSocketConfig() { LogLevel = Discord.LogSeverity.Debug });

            // log in
            await client.LoginAsync(Discord.TokenType.Bot, Program.GlobalConfig.Data.ConnectionToken);
            await client.StartAsync();

            handler = new CommandHandler();
            await handler.Install(client);

            client.Log += Log;

            client.Ready += Client_Ready;

            await client.SetGameAsync("Type: +Help");

            await Task.Delay(-1);
        }

        private async Task Client_Ready()
        {
            var application = await client.GetApplicationInfoAsync();
            await Log(new LogMessage(LogSeverity.Info, "Program",
                $"Invite URL: <https://discordapp.com/oauth2/authorize?client_id={application.Id}&scope=bot>"));
        }

        public async static Task Log(Discord.LogMessage arg)
        {
            Console.WriteLine(arg.ToString());
        }
    }
}
