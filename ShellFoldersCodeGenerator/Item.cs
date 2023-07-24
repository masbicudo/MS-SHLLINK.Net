// https://github.com/libyal/libfwsi/wiki/Shell-Folder-identifiers
// https://docs.rainmeter.net/tips/launching-windows-special-folders/
// https://winaero.com/windows-11-shell-commands-the-complete-list/
// https://gist.github.com/davehull/b6c119e3afd63053bb92?permalink_comment_id=4384945

namespace ShellFoldersCodeGenerator
{
    public class Item
    {
        public Guid guid;
        public HashSet<string> shellNames;
        public string? clsid;
        public HashSet<string> csidls;
        public string? folderid;
        public HashSet<string> descriptions;
        public HashSet<string> os;
        public HashSet<string> sources;

        public Item(
                Guid guid,
                HashSet<string> shellNames,
                string? clsid,
                HashSet<string> csidls,
                string? folderid,
                HashSet<string> descriptions,
                HashSet<string> os,
                HashSet<string> sources
            )
        {
            this.guid = guid;
            this.shellNames = shellNames;
            this.clsid = clsid;
            this.csidls = csidls;
            this.folderid = folderid;
            this.descriptions = descriptions;
            this.os = os;
            this.sources = sources;
        }

        public override string ToString()
        {
            var strOs = string.Join(", ", os);
            var strNames = string.Join(", ", shellNames);
            var strDescs = string.Join(", ", descriptions);
            var strSrc = string.Join(", ", sources);
            var strcsidls = string.Join(", ", csidls);
            var str = $"Item({guid}, {{{strNames}}}, {clsid}, {{{strcsidls}}}, {folderid}, {{{strDescs}}}, {{{strOs}}}, {{{strSrc}}})";
            return str;
        }
    }
}