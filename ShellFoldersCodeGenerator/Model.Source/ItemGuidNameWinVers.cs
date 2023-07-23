namespace ShellFoldersCodeGenerator.Model.Source
{
    public class ItemGuidNameWinVers
    {
        public ItemGuidNameWinVers(string title, string guid, string winvers, string source)
        {
            this.guid = guid;
            this.title = title;
            this.winvers = winvers;
            this.source = source;
        }

        public string title { get; set; }
        public string guid { get; set; }
        public string winvers { get; set; }
        public string source { get; set; }
    }
}