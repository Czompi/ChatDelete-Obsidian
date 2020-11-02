using Newtonsoft.Json;
using System.IO;

namespace hu.czompi.chatdelete
{
    internal class Config: ConfigFile
    {
        public int LineCount { get; internal set; }

        internal void LoadConfig()
        {
            Globals.Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Path.Combine("ChatDelete", "config.json")));
        }

        internal void ReloadConfig()
        {
            SaveDefaultConfig();
            LoadConfig();
        }

        internal void SaveDefaultConfig()
        {
            if (!File.Exists(Path.Combine("ChatDelete", "config.json")))
            {
                Globals.Config = new Config
                {
                    Prefix = "&8[&b ChatDelete &8]&r ",
                    LineCount = 100,
                    Messages = new Messages
                    {
                        Clear = "&6Full chat deleted by&r &c{0}&6.&r",
                        Reload = "&2Config reloaded!&r",
                        Usage = "&4Command usage:&r &c{0}&r"
                    }
                };
            }
        }
    }
}