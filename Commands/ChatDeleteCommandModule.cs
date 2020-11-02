using hu.czompi.chatdelete;
using Newtonsoft.Json;
using Obsidian.Chat;
using Obsidian.CommandFramework.Attributes;
using Obsidian.CommandFramework.Entities;
using Obsidian.Commands;
using Obsidian.Entities;
using Obsidian.Util;
using Obsidian.WorldData;
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
        [Command("cd", "chatdelete", "chatclear","cc")]
        [RequireOperator]
        public async Task ChatDeleteAsync(ObsidianContext Context, EChatDeleteArguments arg0)
        {
            var player = Context.Player;

            var chatMessage = new ChatMessage();
            switch (arg0)
            {
                case EChatDeleteArguments.Delete:
                case EChatDeleteArguments.Clear:
                case EChatDeleteArguments.C:
                    var clr_msg = Globals.Config.Messages.Clear.Split("{0}");
                    chatMessage = new ChatMessage();
                    chatMessage.AddExtra(new ChatMessage { Text = $"{clr_msg[0]}" });
                    //chatMessage.AddExtra(new ChatMessage { Text = $"{ChatColor.Gray}Chat successfully deleted by " });
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
                    chatMessage.AddExtra(new ChatMessage { Text = $"{clr_msg[1]}" });
                    //chatMessage.AddExtra(new ChatMessage { Text = $"{ChatColor.Gray}." });

                    await Context.Server.BroadcastAsync(JsonConvert.SerializeObject(chatMessage));
                    break;
                case EChatDeleteArguments.Help:
                    #region Command list
                    var cmds = new Dictionary<String, String>();
                    cmds.Add("help", "Shows this list.");
                    cmds.Add("clear", "Clears the chat.");
                    cmds.Add("delete", "Clears the chat.");
                    cmds.Add("reload", "Reload plugin configuration.");
                    #endregion

                    chatMessage = new ChatMessage();
                    chatMessage.AddExtra(new ChatMessage { Text = $"{Globals.Config.Prefix}{ChatColor.Gray}Plugin commands:" });

                    #region Build per command message.
                    for (int i = 0; i < cmds.Count; i++)
                    {
                        var cmd = cmds.ToArray()[i];
                        var cmdargname = new ChatMessage
                        {
                            Text = $"{ChatColor.Red}/chatdelete {cmd.Key}{ChatColor.Reset} ",
                            HoverEvent = new TextComponent
                            {
                                Action = ETextAction.ShowText,
                                Value = $"{DateTime.Now.ToShortDateString()}"
                            }
                        };
                        chatMessage.AddExtra(cmdargname);
                        var cmdargdesc = new ChatMessage
                        {
                            Text = $"{ChatColor.Gray}{cmd.Value}{ChatColor.Reset}\n",
                            HoverEvent = new TextComponent
                            {
                                Action = ETextAction.ShowText,
                                Value = $"{DateTime.Now.ToShortDateString()}"
                            }
                        };
                        chatMessage.AddExtra(cmdargdesc);
                    }
                    #endregion

                    break;
                case EChatDeleteArguments.Reload:


                    break;
                default:
                    break;
            }

        }
    }
}
