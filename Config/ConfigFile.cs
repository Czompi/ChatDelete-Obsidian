namespace hu.czompi.chatdelete
{
    public class ConfigFile
    {
        public string Prefix { get; set; }
        //{
        //    get => .Replace("&","§");
        //    set => _prefix = value;
        //}

        public int LineCount { get; set; }

        public Messages Messages { get; set; }
    }

    public class Messages
    {
        private string _clear;
        private string _reload;
        private string _usage;
        public string Clear
        {
            get => _clear.Replace("&", "§");
            set => _clear = value;
        }
        public string Reload
        {
            get => _reload.Replace("&", "§");
            set => _reload = value;
        }
        public string Usage
        {
            get => _usage.Replace("&", "§");
            set => _usage = value;
        }
    }
}