namespace ShellFoldersCodeGenerator.Model.Source
{
    public class ItemShellIdName
    {
        public ItemShellIdName(string shellid, string title, string source)
        {
            this.shellid = shellid;
            this.title = title;
            this.source = source;
        }

        public string shellid { get; set; }
        public string title { get; set; }
        public string source { get; set; }
    }
}