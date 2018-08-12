using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DiscordAchievementBot
{
    [RequireOwner()]
    public class AdminCommands : ModuleBase
    {
        private readonly ImageGenerator generator;

        public AdminCommands(ImageGenerator generator)
        {
            this.generator = generator;
        }

        [Command("Debug")]
        [Summary("Replies back with some debug info about the bot.")]
        public async Task DebugInfo()
        {
            // download all of the users for each guild
            var client = Context.Client as DiscordSocketClient;
            await client.DownloadUsersAsync(client.Guilds).ConfigureAwait(false);

            await ReplyAsync(
                $"{Format.Bold("Info")}\n" +
                $"- D.NET Lib Version {DiscordConfig.Version} (API v{DiscordConfig.APIVersion})\n" +
                $"- Runtime: {RuntimeInformation.FrameworkDescription} {RuntimeInformation.OSArchitecture}\n" +
                $"- Heap: {GetHeapSize()} MB\n" +
                $"- Uptime: {GetUpTime()}\n\n" +
                $"- Guilds: {(Context.Client as DiscordSocketClient).Guilds.Count}\n" +
                $"- Channels: {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Channels.Count)}\n" +
                $"- Users: {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Users.Count)}\n" +
                $"# of images generated = {generator.GenerationCounter}"
                ).ConfigureAwait(false);
        }

        private static readonly IFormatProvider formatProvider = new CultureInfo("en-US");

        private static string GetUpTime()
            => (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss", formatProvider);
        private static string GetHeapSize()
            => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString(formatProvider);
    }
}
