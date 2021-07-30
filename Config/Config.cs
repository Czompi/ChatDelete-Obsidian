using System.IO;
using System.Text.Json;

namespace hu.czompi.chatdelete
{
    internal class ConfigManager : ConfigFile
    {


        public ChatDeletePlugin Plugin { get; }

        internal ConfigManager(ChatDeletePlugin plugin) => Plugin = plugin;

        internal void LoadConfig()
        {
            Globals.Config = JsonSerializer.Deserialize<ConfigFile>(Plugin.FileReader.ReadAllText(Path.Combine("ChatDelete", "config.json")));
        }

        internal void ReloadConfig()
        {
            try
            {
                SaveDefaultConfig();
            }
            catch (System.Exception ex)
            {
                Plugin.Logger.LogError(ex);
            }

            try
            {
                LoadConfig();
            }
            catch (System.Exception ex)
            {
                Plugin.Logger.LogError(ex);
            }
        }

        internal void SaveDefaultConfig()
        {
            if (!File.Exists(Path.Combine("ChatDelete", "config.json")))
            {
                Globals.Config = new ConfigFile
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
                Plugin.FileWriter.WriteAllText(Path.Combine("ChatDelete", "config.json"), JsonSerializer.Serialize(Globals.Config));
            }
        }
    }
}