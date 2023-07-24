namespace ShellFoldersCodeGenerator.Model.Source
{
    public class ItemShellNameTitle
    {
        public ItemShellNameTitle(string shellName, string title, string source)
        {
            this.shellName = shellName;
            this.title = title;
            this.source = source;
        }

        public string shellName { get; set; }
        public string title { get; set; }
        public string source { get; set; }
    }
}