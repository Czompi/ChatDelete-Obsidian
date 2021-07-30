using hu.czompi.chatdelete.commands;
using Obsidian.API;
using Obsidian.API.Plugins;
using Obsidian.API.Plugins.Services;
using System;
using System.Threading.Tasks;

namespace hu.czompi.chatdelete
{
    [Plugin(Name = "ChatDelete", Version = "1.0.0",
            Authors = "Czompi", Description = "Clears the chat.",
            ProjectUrl = "https://github.com/Czompi/ChatDelete-Obsidian")]
    public class ChatDeletePlugin : PluginBase
    {
        [Inject] public ILogger Logger { get; set; }
        [Inject] public IFileReader FileReader { get; set; }
        [Inject] public IFileWriter FileWriter { get; set; }
        
        internal ConfigManager Config { get; private set; }

        public async Task OnLoad(IServer server)
        {
            //server.<ChatDeleteCommandModule>();
            Config = new ConfigManager(this);
            Config.ReloadConfig();
            Logger.Log("ChatDelete loaded!");
            await Task.CompletedTask;
        }

        public async Task OnServerTick()
        {
            await Task.CompletedTask;
        }
    }
}