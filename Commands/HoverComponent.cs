using Obsidian.API;
using System;
using System.Text.Json.Serialization;

namespace CzomPack.Obsidian
{
    internal class HoverComponent : IHoverComponent
    {
        private IHoverComponent _hoverComponent;
        internal HoverComponent() => _hoverComponent = IHoverComponent.CreateNew();
        internal HoverComponent(IHoverComponent hoverComponent) => _hoverComponent = hoverComponent;
        internal IHoverComponent GetInterface() => _hoverComponent;

        [JsonPropertyName("action")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EHoverAction Action { get => _hoverComponent.Action; set => _hoverComponent.Action = value; }
        public object Contents { get => _hoverComponent.Contents; set => _hoverComponent.Contents = value; }
        public string Translate { get => _hoverComponent.Translate; set => _hoverComponent.Translate = value; }

    }
}