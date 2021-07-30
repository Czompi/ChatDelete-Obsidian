using Obsidian.API;

namespace hu.czompi.chatdelete.commands
{
    internal class ClickComponent : IClickComponent
    {
        private IClickComponent _clickComponent;
        internal ClickComponent() => _clickComponent = IClickComponent.CreateNew();
        internal ClickComponent(IClickComponent clickComponent) => _clickComponent = clickComponent;
        internal IClickComponent GetInterface() => _clickComponent;

        public EClickAction Action { get => _clickComponent.Action; set => _clickComponent.Action = value; }
        public string Value { get => _clickComponent.Value; set => _clickComponent.Value = value; }
        public string Translate { get => _clickComponent.Translate; set => _clickComponent.Translate = value; }
    }
}