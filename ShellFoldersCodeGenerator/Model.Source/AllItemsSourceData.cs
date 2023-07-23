namespace ShellFoldersCodeGenerator.Model.Source
{
    public class AllItemsSourceData
    {
        public List<ItemGuidWinVersClsIdDescription> libfwsi_Items { get; set; } = new();
        public List<ItemGuidName> RainMeterTitleGuid { get; set; } = new();
        public List<ItemShellIdName> RainMeterShellIdTitle { get; set; } = new();
        public List<ItemGuidName> WinAeroGuidTitle { get; set; } = new();
        public List<ItemShellIdName> WinAeroShellIdTitle { get; set; } = new();
        public List<ItemGuidName> DaveHullGuidTitle { get; set; } = new();
        public List<ItemGuidName> EricZimmermanGuidTitle { get; set; } = new();
        public List<ItemGuidNameWinVers> Microsoft2013 { get; set; } = new();
        public List<ItemGuidName> Microsoft2022 { get; set; } = new();
        public List<ItemKnownFolderId> MicrosoftKnownFolderId { get; set; } = new();
    }
}