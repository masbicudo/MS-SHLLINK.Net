namespace ShellFoldersCodeGenerator.Model.Source
{
    public class ItemGuidTitle
    {
        public ItemGuidTitle(string title, string guid, string source)
        {
            this.guid = guid;
            this.title = title;
            this.source = source;
        }

        public string title { get; set; }
        public string guid { get; set; }
        public string source { get; set; }
    }
}