﻿namespace ShellFoldersCodeGenerator.Model.Source
{
    public class ItemKnownFolderId
    {
        public ItemKnownFolderId(
                string folderid,
                string guid,
                string displayName,
                string folderType,
                string defaultPath,
                string[] csidls,
                string legacyDisplayName,
                string legacyDefaultPath,
                HashSet<string> os
            )
        {
            this.folderid = folderid;
            this.guid = guid;
            this.displayName = displayName;
            this.folderType = folderType;
            this.defaultPath = defaultPath;
            this.csidls = csidls;
            this.legacyDisplayName = legacyDisplayName;
            this.legacyDefaultPath = legacyDefaultPath;
            this.os = os;
        }

        public string folderid { get; set; }
        public string guid { get; set; }
        public string displayName { get; set; }
        public string folderType { get; set; }
        public string defaultPath { get; set; }
        public string[] csidls { get; set; }
        public string legacyDisplayName { get; set; }
        public string legacyDefaultPath { get; set; }
        public HashSet<string> os { get; set; }
    }

}