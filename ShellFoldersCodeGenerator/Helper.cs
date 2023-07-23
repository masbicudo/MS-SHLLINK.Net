// https://github.com/libyal/libfwsi/wiki/Shell-Folder-identifiers
// https://docs.rainmeter.net/tips/launching-windows-special-folders/
// https://winaero.com/windows-11-shell-commands-the-complete-list/
// https://gist.github.com/davehull/b6c119e3afd63053bb92?permalink_comment_id=4384945

using ShellFoldersCodeGenerator.Model.Source;
using System.Text.RegularExpressions;

namespace ShellFoldersCodeGenerator
{
    internal static class Helper
    {
        public static void AddItem(
                AllItemsData data,
                Guid guid,
                HashSet<string> names,
                string clsid,
                string description,
                HashSet<string> os,
                string source
            )
        {
            if (data.byGuid.TryGetValue(guid, out int index))
            {
                data.allItems[index].os.AddMany(os);
                data.allItems[index].descriptions.Add(description);
                data.allItems[index].names.AddMany(names);
                data.allItems[index].sources.Add(source);
            }
            else
            {
                data.allItems.Add(new(
                    guid,
                    names,
                    clsid,
                    new HashSet<string> { description },
                    os,
                    new HashSet<string> { source }));
                index = data.allItems.Count - 1;
                data.byGuid.Add(guid, index);
            }

            HashSet<string> words;
            foreach (var name in names)
            {
                if (!data.byName.TryGetValue(name, out var indexes))
                {
                    indexes = new HashSet<int>();
                    data.byName.Add(name, indexes);
                }
                indexes.Add(index);

                words = new HashSet<string>(
                        Regex.Split(name, @"[^\w\d_]+|(?:(?<!3)|(?!D))(?:\b|(?<=[a-z0-9])(?=[A-Z])|(?=[A-Z][a-z]))")
                            .Where(w => !string.IsNullOrWhiteSpace(w)),
                        StringComparer.InvariantCultureIgnoreCase
                    );
                AddWords(data, words, index);
            }

            words = new HashSet<string>(
                    Regex.Split(description, @"[^\w\d_]+"),
                    StringComparer.InvariantCultureIgnoreCase
                );
            AddWords(data, words, index);
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
                    Regex.Split(description, @"[^\w\d_]+"),
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
    }
}