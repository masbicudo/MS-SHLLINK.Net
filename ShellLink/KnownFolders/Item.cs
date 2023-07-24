using System;
using System.Collections.Generic;

namespace ShellLink.KnownFolders
{
    public class KnownFolderInfo
    {
        public Guid guid;
        public HashSet<string> shellNames;
        public string? clsid;
        public string? csidl;
        public string? folderid;
        public HashSet<string> descriptions;
        public HashSet<string> os;
        public HashSet<string> sources;

        public KnownFolderInfo(
                Guid guid,
                HashSet<string> shellNames,
                string? clsid,
                string? csidl,
                string? folderid,
                HashSet<string> descriptions,
                HashSet<string> os,
                HashSet<string> sources
            )
        {
            this.guid = guid;
            this.shellNames = shellNames;
            this.clsid = clsid;
            this.csidl = csidl;
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
            var str = $"Item({guid}, {{{strNames}}}, {clsid}, {csidl}, {folderid}, {{{strDescs}}}, {{{strOs}}}, {{{strSrc}}})";
            return str;
        }
    }
}
