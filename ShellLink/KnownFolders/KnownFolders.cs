using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShellLink.KnownFolders
{
    public static partial class KnownFolders
    {
        private static AllItemsData data = ReadDataAsync().Result;
        private static async Task<AllItemsData> ReadDataAsync()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ShellLink.KnownFolders.KnownFolders.AllKFData.json"))
            {
                var data = await JsonSerializer.DeserializeAsync<AllItemsData>(stream);
                return data;
            }
        }
        private static string[] SplitWords(string text)
        {
            var result = Regex.Split(text, @"[^\w\d_]+|(?:(?<!3)|(?!D))(?:\b|(?<=[a-z0-9])(?=[A-Z])|(?=[A-Z][a-z]))")
                .Where(w => !string.IsNullOrWhiteSpace(w)).ToArray();
            return result;
        }
        private static IEnumerable<KnownFolderInfo> GetManyByIds(IEnumerable<int> ids)
        {
            foreach (var id in ids)
                yield return data.allItems[id];
        }
        private static KnownFolderInfo[] GetManyByIds(int[] ids)
        {
            var result = new KnownFolderInfo[ids.Length];
            for (int it = 0; it < ids.Length; it++)
            {
                int id = ids[it];
                result[it] = data.allItems[id];
            }
            return result;
        }
        public static IEnumerable<KnownFolderInfo> SearchKnownFolders(string query)
        {
            var words = new HashSet<string>(
                    SplitWords(query),
                    StringComparer.InvariantCultureIgnoreCase
                );
            var sets = words
                .Where(w => data.byDescription.ContainsKey(w))
                .Select(w => data.byDescription[w])
                .ToArray();
            if (sets.Length > 0)
            {
                var finalSet = sets.First();
                foreach (var set in sets.Skip(1))
                    finalSet = new HashSet<int>(finalSet.Intersect(set));
                foreach (var idx in finalSet)
                    yield return data.allItems[idx];
            }
        }
        public static KnownFolderInfo GetKnownFolderByGuid(Guid guid)
        {
            var result = data.allItems[data.byGuid[guid]];
            return result;
        }
        public static IEnumerable<KnownFolderInfo> GetKnownFolderByShellName(string shellName)
        {
            return GetManyByIds(data.byName[shellName]);
        }
        public static IEnumerable<KnownFolderInfo> GetKnownFolderByClsId(string clsid)
        {
            return GetManyByIds(data.byClsId[clsid]);
        }
        public static IEnumerable<KnownFolderInfo> GetKnownFolderByCSIdL(string csidl)
        {
            return GetManyByIds(data.byCSIdL[csidl]);
        }
        public static IEnumerable<KnownFolderInfo> GetKnownFolderByFolderId(string folderid)
        {
            return GetManyByIds(data.byFolderId[folderid]);
        }
    }
}
