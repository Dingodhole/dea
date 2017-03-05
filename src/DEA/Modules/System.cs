﻿using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DEA.SQLite.Models;
using DEA.SQLite.Repository;

namespace System.Modules
{
    public class System : ModuleBase<SocketCommandContext>
    {

        private DbContext _db;
        private Process _process;
        private CommandService _service;

        protected override void BeforeExecute()
        {
            _process = Process.GetCurrentProcess();
            _db = new DbContext();
        }

        protected override void AfterExecute()
        {
            _db.Dispose();
        }

        public System(CommandService service)           
        {
            _service = service;
        }

        [Command("Help")]
        [Alias("commands", "cmd", "cmds", "command")]
        [Remarks("All command information.")]
        public async Task HelpAsync()
        {
            var guildRepo = new GuildRepository(_db);
            string message = null;
            int longest = 0;

            foreach (var module in _service.Modules)
                foreach (var cmd in module.Commands)
                    if (cmd.Aliases.First().Length > longest) longest = cmd.Aliases.First().Length;

            foreach (var module in _service.Modules)
            {
                message += $"**{module.Name} Commands **: ```asciidoc\n";
                foreach (var cmd in module.Commands)
                {
                        message += $"{await guildRepo.GetPrefix(Context.Guild.Id)}{cmd.Aliases.First()}{new String(' ', (longest + 1) - cmd.Aliases.First().Length)} :: {cmd.Remarks}\n";
                }

                message += "```\n ";
            }
            try
            {
                var channel = await Context.User.CreateDMChannelAsync();
                await channel.SendMessageAsync(message);
                await ReplyAsync($"{Context.User.Mention}, you have been DMed with all the command information!");
            } catch (Exception e)
            {
                await ReplyAsync($"{Context.User.Mention}, {e.Message}");
            }
   
        }

        [Command("Stats")]
        [Alias("statistics")]
        [Remarks("All statistics about DEA.")]
        public async Task Info()
        {
            var uptime = (DateTime.Now - _process.StartTime);
            var application = await Context.Client.GetApplicationInfoAsync();
            var message =
                $"```asciidoc\n= STATISTICS =\n" +
                $"• Memory   :: {(_process.PrivateMemorySize64 / 1000000f).ToString("0.##")} MB\n" +
                $"• Uptime   :: Days: {uptime.Days}, Hours: {uptime.Hours}, Minutes: {uptime.Minutes}, Seconds: {uptime.Seconds}\n" +
                $"• Users    :: {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Users.Count)}\n" +
                $"• Servers  :: {(Context.Client as DiscordSocketClient).Guilds.Count}\n" +
                $"• Channels :: {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Channels.Count)}\n" +
                $"• Library  :: Discord.Net {DiscordConfig.Version}\n" +
                $"• Runtime  :: {RuntimeInformation.FrameworkDescription} {RuntimeInformation.OSArchitecture}```";
            try
            {
                var channel = await Context.User.CreateDMChannelAsync();
                await channel.SendMessageAsync(message);
                await ReplyAsync($"{Context.User.Mention}, you have been DMed with all the statistics!");
            }
            catch (Exception e)
            {
                await ReplyAsync($"{Context.User.Mention}, {e.Message}");
            }
            
        }
    }
}