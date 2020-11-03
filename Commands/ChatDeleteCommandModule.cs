using hu.czompi.chatdelete;
using Newtonsoft.Json;
using Obsidian.API;
using Obsidian.API.CommandFramework.Attributes;
using Obsidian.API.CommandFramework.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hu.czompi.chatdelete.commands
{
    public class ChatDeleteCommandModule : BaseCommandClass
    {
		
        #region chatdelete
        [Command("chatdelete", "cd", "chatclear", "cc")]
        [RequireOperator]
        public async Task ChatDeleteAsync(ObsidianContext Context, string arg0="")
        {
            var player = Context.Player;

            var chatMessage = new ChatMessage();
            var prefix = "&8[&b ChatDelete &8]&r ";
            prefix = prefix.Replace("&", "§");
            switch (arg0.ToString().ToLower())
            {
                case "delete":
                    var clear = "";
                    for (int i = 0; i < 100; i++)
                    {
                        clear += "\n";
                    }
                    Context.Server.BroadcastAsync(clear);
                    //var clr_msg = Globals.Config.Messages.Clear.Split("{0}");
                    chatMessage = ChatMessage.Simple("");
                    //chatMessage.AddExtra(new ChatMessage { Text = $"{clr_msg[0]}" });
                    chatMessage.AddExtra(new ChatMessage { Text = $"{prefix}{ChatColor.Gray}Chat successfully deleted by " });
                    var user = new ChatMessage
                    {
                        Text = $"{ChatColor.Red}{player.Username}{ChatColor.Gray}",
                        HoverEvent = new TextComponent
                        {
                            Action = ETextAction.ShowText,
                            Value = $"{DateTime.Now.ToShortDateString()}"
                        }
                    };
                    chatMessage.AddExtra(user);
                    //chatMessage.AddExtra(new ChatMessage { Text = $"{clr_msg[1]}" });
                    chatMessage.AddExtra(new ChatMessage { Text = $"{ChatColor.Gray}." });
                    foreach (var onlinePlayer in Context.Server.OnlinePlayers)
                    {
                        await Context.Server.OnlinePlayers[onlinePlayer.Key].SendMessageAsync(chatMessage);
                    }
                    break;
                default:
                case "help":
                    #region Command list
                    var cmds = new Dictionary<String, String>();
                    cmds.Add("help", "Shows this list.");
                    cmds.Add("clear", "Clears the chat.");
                    cmds.Add("delete", "Clears the chat.");
                    cmds.Add("reload", "Reload plugin configuration.");
                    #endregion

                    chatMessage = ChatMessage.Simple("");
                    //chatMessage.AddExtra(new ChatMessage { Text = $"{Globals.Config.Prefix}{ChatColor.Gray}Plugin commands:" });
                    chatMessage.AddExtra(new ChatMessage { Text = $"{prefix}{ChatColor.Gray}Plugin commands:\n" });

                    #region Build per command message.
                    for (int i = 0; i < cmds.Count; i++)
                    {
                        var cmd = cmds.ToArray()[i];
                        var cmdargname = new ChatMessage
                        {
                            Text = $"{ChatColor.Red}/chatdelete {cmd.Key}{ChatColor.Reset} ",
                            ClickEvent = new TextComponent
                            {
                                Action = ETextAction.SuggestCommand,
                                Value = $"/chatdelete {cmd.Key}"
                            }
                        };
                        chatMessage.AddExtra(cmdargname);
                        var cmdargdesc = new ChatMessage
                        {
                            Text = $"{ChatColor.Gray}{cmd.Value}{ChatColor.Reset}{(i+1==cmds.Count ? "":"\n")}"
                        };
                        chatMessage.AddExtra(cmdargdesc);
                    }
                    #endregion
                    await Context.Player.SendMessageAsync(chatMessage);
                    break;
                case "reload":


                    break;
            }

        }
        #endregion

	}
}
