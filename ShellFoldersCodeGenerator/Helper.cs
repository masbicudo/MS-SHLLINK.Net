// https://github.com/libyal/libfwsi/wiki/Shell-Folder-identifiers
// https://docs.rainmeter.net/tips/launching-windows-special-folders/
// https://winaero.com/windows-11-shell-commands-the-complete-list/
// https://gist.github.com/davehull/b6c119e3afd63053bb92?permalink_comment_id=4384945

using ShellFoldersCodeGenerator.Model.Source;
using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ShellFoldersCodeGenerator
{
    internal static class Helper
    {
        public static void AddItem(
                AllItemsData data,
                Guid guid,
                HashSet<string> shellNames,
                string? clsid,
                HashSet<string> csidls,
                string? folderid,
                string description,
                HashSet<string> os,
                string source
            )
        {
            if (data.byGuid.TryGetValue(guid, out int index))
            {
                var item = data.allItems[index];
                item.os.AddMany(os);
                item.descriptions.Add(description);
                item.shellNames.AddMany(shellNames);
                item.csidls.AddMany(csidls);
                if (clsid != null)
                {
                    if (item.clsid != null && item.clsid != clsid)
                        throw new Exception("Item already contains a different clsid");
                    item.clsid = clsid;
                }
                if (folderid != null)
                {
                    if (item.folderid != null && item.folderid != folderid)
                        throw new Exception("Item already contains a different folderid");
                    item.folderid = folderid;
                }
                item.sources.Add(source);
            }
            else
            {
                data.allItems.Add(new(
                    guid,
                    shellNames,
                    clsid,
                    csidls,
                    folderid,
                    new HashSet<string> { description },
                    os,
                    new HashSet<string> { source }));
                index = data.allItems.Count - 1;
                data.byGuid.Add(guid, index);
            }

            HashSet<string> words;

            if (clsid != null)
            {
                data.byClsId.DictListAdd(clsid, index);

                words = new HashSet<string>(
                        clsid.Replace("CLSID_", "").SplitWords(),
                        StringComparer.InvariantCultureIgnoreCase
                    );
                AddWords(data, words, index);
            }

            foreach (var csidl in csidls)
            {
                data.byCSIdL.DictListAdd(csidl, index);

                words = new HashSet<string>(
                        csidl.Replace("CSIDL_", "").SplitWords(),
                        StringComparer.InvariantCultureIgnoreCase
                    );
                AddWords(data, words, index);
            }

            if (folderid != null)
            {
                data.byFolderId.DictListAdd(folderid, index);

                words = new HashSet<string>(
                        folderid.Replace("FOLDERID_", "").SplitWords(),
                        StringComparer.InvariantCultureIgnoreCase
                    );
                AddWords(data, words, index);
            }

            foreach (var name in shellNames)
            {
                data.byShellName.DictListAdd(name, index);

                words = new HashSet<string>(
                        name.SplitWords(),
                        StringComparer.InvariantCultureIgnoreCase
                    );
                AddWords(data, words, index);
            }

            if (clsid != null)
            {

            }

            words = new HashSet<string>(
                    description.SplitWords(),
                    StringComparer.InvariantCultureIgnoreCase
                );
            AddWords(data, words, index);
        }
        internal static void DictListAdd<TKey, TValue>(this Dictionary<TKey, List<TValue>> dictLists, TKey key, TValue value)
        {
            if (!dictLists.TryGetValue(key, out var listValues))
                dictLists.Add(key, listValues = new List<TValue>());
            listValues.Add(value);
        }
        internal static void DictListAdd<TKey, TValue>(this Dictionary<TKey, HashSet<TValue>> dictSets, TKey key, TValue value)
        {
            if (!dictSets.TryGetValue(key, out var listValues))
                dictSets.Add(key, listValues = new HashSet<TValue>());
            listValues.Add(value);
        }
        public static string[] SplitWords(this string text)
        {
            var result = Regex.Split(text, @"[^\w\d_]+|(?:(?<!3)|(?!D))(?:\b|(?<=[a-z0-9])(?=[A-Z])|(?=[A-Z][a-z]))")
                .Where(w => !string.IsNullOrWhiteSpace(w)).ToArray();
            return result;
        }
        public static void AddWords(AllItemsData data, IEnumerable<string> words, int index)
        {
            foreach (var word in words)
            {
                if (!data.byDescription.TryGetValue(word, out var set))
                {
                    set = new HashSet<int>();
                    data.byDescription.Add(word, set);
                }
                set.Add(index);
            }
        }

        public static IEnumerable<Item> GetGuidsByDescription(AllItemsData data, string description)
        {
            var words = new HashSet<string>(
                    description.SplitWords(),
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
                {
                    finalSet = new HashSet<int>(finalSet.Intersect(set));
                }
                foreach (var idx in finalSet)
                    yield return data.allItems[idx];
            }
        }

        public static void AddMany<T>(this HashSet<T> set, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                set.Add(item);
            }
        }

        public static async Task<string> DownloadAsync(string url, string cacheFileName)
        {
            var cachePathAndFileName = Path.Join("cache", cacheFileName);
            if (File.Exists(cachePathAndFileName))
            {
                var contents = await File.ReadAllTextAsync(cachePathAndFileName);
                return contents;
            }
            using (var client = new HttpClient())
            {
                var contents = await client.GetStringAsync(url);
                if (!Directory.Exists("cache"))
                    Directory.CreateDirectory("cache");
                await File.WriteAllTextAsync(cachePathAndFileName, contents);
                return contents;
            }
        }

        public static string FindFilePath(string relativePath)
        {
            var currentFolder = Path.GetFullPath(".");
            while (!File.Exists(Path.Combine(currentFolder, relativePath)))
            {
                currentFolder = Path.GetDirectoryName(currentFolder);
            }
            return Path.Combine(currentFolder, relativePath);
        }
        public static string Indent(this string text, string indent)
        {
            var result = Regex.Replace(text, @"\n|\r\n", m => m.Groups[0] + indent);
            return result;
        }
    }
}