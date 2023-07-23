namespace ShellFoldersCodeGenerator.Model.Source
{
    public class AllItemsData
    {
        public List<Item> allItems { get; set; } = new();
        public Dictionary<string, HashSet<int>> byDescription { get; set; } = new(StringComparer.InvariantCultureIgnoreCase);
        public Dictionary<Guid, int> byGuid { get; set; } = new();
        public Dictionary<string, HashSet<int>> byName { get; set; } = new();
    }
}