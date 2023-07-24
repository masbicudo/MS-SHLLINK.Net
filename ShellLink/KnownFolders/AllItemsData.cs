using System;
using System.Collections.Generic;

namespace ShellLink.KnownFolders
{
    public class AllItemsData
    {
        public List<KnownFolderInfo> allItems { get; set; } = new();
        public Dictionary<string, HashSet<int>> byDescription { get; set; } = new(StringComparer.InvariantCultureIgnoreCase);
        public Dictionary<Guid, int> byGuid { get; set; } = new();
        public Dictionary<string, HashSet<int>> byName { get; set; } = new();
        public Dictionary<string, HashSet<int>> byClsId { get; set; } = new();
        public Dictionary<string, HashSet<int>> byCSIdL { get; set; } = new();
        public Dictionary<string, HashSet<int>> byFolderId { get; set; } = new();
        public Dictionary<string, HashSet<int>> bySource { get; set; } = new();
    }
}
