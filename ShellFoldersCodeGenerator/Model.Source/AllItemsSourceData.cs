namespace ShellFoldersCodeGenerator.Model.Source
{
    public class AllItemsSourceData
    {
        public List<ItemGuidWinVersClsIdDescription> libfwsi_Items { get; set; } = new();
        public List<ItemGuidTitle> RainMeterTitleGuid { get; set; } = new();
        public List<ItemShellNameTitle> RainMeterShellIdTitle { get; set; } = new();
        public List<ItemGuidTitle> WinAeroGuidTitle { get; set; } = new();
        public List<ItemShellNameTitle> WinAeroShellIdTitle { get; set; } = new();
        public List<ItemGuidTitle> DaveHullGuidTitle { get; set; } = new();
        public List<ItemGuidTitle> EricZimmermanGuidTitle { get; set; } = new();
        public List<ItemGuidNameWinVers> Microsoft2013 { get; set; } = new();
        public List<ItemGuidTitle> Microsoft2022 { get; set; } = new();
        public List<ItemKnownFolderId> MicrosoftKnownFolderId { get; set; } = new();
    }
}