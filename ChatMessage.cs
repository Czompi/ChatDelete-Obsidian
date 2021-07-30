using ChatDelete.Extensions;
using Obsidian.API;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CzomPack.Obsidian
{
    /// <summary>
    /// 
    /// </summary>
    public class ChatMessage : IChatMessage
    {
        private IChatMessage _chatMessage;
        public ChatMessage()
        {
            _chatMessage = IChatMessage.Simple("");
        }
        
        public ChatMessage(IChatMessage chatMessage)
        {
            SetInterface(chatMessage);
        }

        public string Text { get => _chatMessage.Text; set => _chatMessage.Text = value.FixColorCodes(); }
        public HexColor Color { get => _chatMessage.Color; set => _chatMessage.Color = value; }
        public bool Bold { get => _chatMessage.Bold; set => _chatMessage.Bold = value; }
        public bool Italic { get => _chatMessage.Italic; set => _chatMessage.Italic = value; }
        public bool Underline { get => _chatMessage.Underline; set => _chatMessage.Underline = value; }
        public bool Strikethrough { get => _chatMessage.Strikethrough; set => _chatMessage.Strikethrough = value; }
        public bool Obfuscated { get => _chatMessage.Obfuscated; set => _chatMessage.Obfuscated = value; }
        public string Insertion { get => _chatMessage.Insertion; set => _chatMessage.Insertion = value; }
        public IClickComponent ClickEvent { get => _chatMessage.ClickEvent; set => _chatMessage.ClickEvent = value; }
        public IHoverComponent HoverEvent { get => _chatMessage.HoverEvent; set => _chatMessage.HoverEvent = value; }

        public IEnumerable<IChatMessage> Extras => _chatMessage.Extras;

        public IChatMessage AddExtra(IChatMessage chatMessage) => _chatMessage.AddExtra(chatMessage);
        public ChatMessage AddExtra(ChatMessage chatMessage) => new(_chatMessage.AddExtra(chatMessage.GetInterface()));

        public IChatMessage AddExtra(IEnumerable<IChatMessage> chatMessages) => _chatMessage.AddExtra(chatMessages);
        public ChatMessage AddExtra(IEnumerable<ChatMessage> chatMessages) => new(_chatMessage.AddExtra(chatMessages.Select(x=>x.GetInterface())));
        public IChatMessage GetInterface() => _chatMessage;
        public void SetInterface(IChatMessage chatMessage) => this._chatMessage = chatMessage;
    }
}
