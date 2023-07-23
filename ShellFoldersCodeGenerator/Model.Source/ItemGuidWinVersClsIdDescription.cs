namespace ShellFoldersCodeGenerator.Model.Source
{
    public class ItemGuidWinVersClsIdDescription
    {
        public ItemGuidWinVersClsIdDescription(string guid, string winvers, string clsid, string description, string source)
        {
            this.guid = guid;
            this.winvers = winvers;
            this.clsid = clsid;
            this.description = description;
            this.source = source;
        }

        public string guid { get; set; }
        public string winvers { get; set; }
        public string clsid { get; set; }
        public string description { get; set; }
        public string source { get; set; }
    }
}