using CzomPack.Obsidian;
using hu.czompi.chatdelete;
using Obsidian.API;
using Obsidian.API.Plugins;
using Obsidian.API.Plugins.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace hu.czompi.chatdelete.commands
{
    [CommandRoot]
    public class ChatDeleteCommand
    {
        [Inject]
        public ChatDeletePlugin Plugin { get; set; }

        [Command("chatdelete", "cd")]
        [CommandInfo("Clears the chat.", "/chatdelete <help|clear|reload>")]
        //[RequirePermission(PermissionCheckType.All, true)]
        public async Task ChatDeleteAsync(CommandContext Context, string arg0)
        {
            Plugin.Logger.Log("Initializing command execution...");
            var player = Context.Player;

            ChatMessage chatMessage = new ChatMessage();
            switch (Enum.Parse<EChatDeleteArguments>(arg0, true))
            {
                case EChatDeleteArguments.Delete:
                case EChatDeleteArguments.Clear:
                case EChatDeleteArguments.C:
                    var clr_msg = Globals.Config.Messages.Clear.Split("{0}");
                    chatMessage = new ChatMessage();
                    Plugin.Logger.Log(JsonSerializer.Serialize(chatMessage));
                    chatMessage.AddExtra(new ChatMessage { Text = $"{new String('\n', Globals.Config.LineCount)}" });
                    chatMessage.AddExtra(new ChatMessage { Text = $"{clr_msg[0]}" });
                    Plugin.Logger.Log(JsonSerializer.Serialize(chatMessage));
                    //chatMessage.AddExtra(new ChatMessage { Text = $"{ChatColor.Gray}Chat successfully deleted by " });
                    var user = new ChatMessage
                    {
                        Text = $"{ChatColor.Red}{player.Username}{ChatColor.Gray}",
                        HoverEvent = new HoverComponent
                        {
                            Action = EHoverAction.ShowText,
                            Contents = $"{DateTime.Now.ToShortDateString()}"
                        }.GetInterface()
                    };
                    chatMessage.AddExtra(user);
                    chatMessage.AddExtra(IChatMessage.Simple($"{clr_msg[1]}"));
                    //chatMessage.AddExtra(new ChatMessage { Text = $"{ChatColor.Gray}." });

                    Plugin.Logger.Log(JsonSerializer.Serialize(chatMessage));
                    await Context.Server.BroadcastAsync(chatMessage.GetInterface());
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
                            Text = $"\n{ChatColor.Red}/chatdelete {cmd.Key}{ChatColor.Reset} ",
                            HoverEvent = new HoverComponent
                            {
                                Action = EHoverAction.ShowText,
                                Contents = $"Click here to suggest command."
                            }.GetInterface(),
                            ClickEvent = new ClickComponent
                            {
                                Action = EClickAction.SuggestCommand,
                                Value = $"/chatdelete {cmd.Key}"
                            }.GetInterface()
                        };
                        chatMessage.AddExtra(cmdargname);
                        var cmdargdesc = new ChatMessage
                        {
                            Text = $"{ChatColor.Gray}{cmd.Value}{ChatColor.Reset}"
                        };
                        chatMessage.AddExtra(cmdargdesc);
                    }
                    Plugin.Logger.Log(JsonSerializer.Serialize(chatMessage));
                    await player.SendMessageAsync(chatMessage.GetInterface());
                    #endregion

                    break;
                case EChatDeleteArguments.Reload:
                    chatMessage = new ChatMessage();
                    Plugin.Logger.Log(JsonSerializer.Serialize(chatMessage));

                    break;
                default:
                    chatMessage = new ChatMessage();
                    Plugin.Logger.Log(JsonSerializer.Serialize(chatMessage));
                    break;
            }

            await Task.CompletedTask;
        }
    }
}
