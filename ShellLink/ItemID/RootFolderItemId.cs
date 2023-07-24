using System;
using System.ComponentModel.Design;

namespace ShellLink.ItemID
{
    public sealed class RootFolderShellItem : TypeIndicatorBasedItemID
    {
        public RootFolderShellItem() {
            this.TypeIndicator = ItemIDTypeIndicator.RootFolder;
        }
        public byte SortIndex { get; set; }

        /// <summary>
        /// Shell folder identifier
        /// For a list of shell folder identifiers see: 
        /// <a href="https://github.com/libyal/libfwsi/wiki/Shell-Folder-identifiers">[LIBFWSI-WIKI]</a>
        /// </summary>
        public Guid ShellFolderIdentifier { get; set; }
        public ExtensionBlock_0xbeef0017 ExtensionBlock { get; set; }
    }
}