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
        // Any interface from Obsidian.Plugins.Services can be injected into properties
        [Inject] public ILogger Logger { get; set; }
        [Inject] public IFileReader FileReader { get; set; }
        [Inject] public IFileWriter FileWriter { get; set; }
        internal Config Config { get; private set; }

        // One of server messages, called when an event occurs
        public async Task OnLoad(IServer server)
        {
            //server.RegisterCommandClass<ChatDeleteCommandModule>();
            Config = new Config();
            Config.ReloadConfig();
            Logger.Log("ChatDelete loaded!");
            await Task.CompletedTask;
        }

        public async Task OnServerTick()
        {
            await Task.CompletedTask;
        }
    }



    public class MyWrapper : PluginWrapper
    {
        public Action Step { get; set; }
        [Alias("get_StepCount")] private Func<int> GetStepCount { get; set; }
        [Alias("set_StepCount")] private Action<int> SetStepCount { get; set; }

        public int StepCount { get => GetStepCount(); set => SetStepCount(value); }
    }
}