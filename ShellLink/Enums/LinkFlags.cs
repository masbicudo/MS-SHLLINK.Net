using System;

namespace ShellLink.DataObjects
{
    /// <summary>
    /// The LinkFlags structure defines bits that specify
    /// which shell link structures are present in the file
    /// format after the ShellLinkHeader structure (section 2.1).
    /// </summary>
    [Flags]
    public enum LinkFlags
    {
        /// <summary>
        /// The shell link is saved with an item ID list (IDList). If this bit is set, a
        /// LinkTargetIDList structure (section 2.2) MUST follow the ShellLinkHeader. 
        /// If this bit is not set, this structure MUST NOT be present.
        /// </summary>
        HasLinkTargetIDList = 1 << 0,

        /// <summary>
        /// The shell link is saved with link information. If this bit is set, a LinkInfo 
        /// structure (section 2.3) MUST be present. If this bit is not set, this structure 
        /// MUST NOT be present.
        /// </summary>
        HasLinkInfo = 1 << 1,

        /// <summary>
        /// The shell link is saved with a name string. If this bit is set, a
        /// NAME_STRING StringData structure (section 2.4) MUST be present. If
        /// this bit is not set, this structure MUST NOT be present.
        /// </summary>
        HasName = 1 << 2,

        /// <summary>
        /// The shell link is saved with a relative path string. If this bit is set, a
        /// RELATIVE_PATH StringData structure (section 2.4) MUST be present. If
        /// this bit is not set, this structure MUST NOT be present.
        /// </summary>
        HasRelativePath = 1 << 3,

        /// <summary>
        /// The shell link is saved with a working directory string. If this bit is set, a
        /// WORKING_DIR StringData structure (section 2.4) MUST be present. If
        /// this bit is not set, this structure MUST NOT be present.
        /// </summary>
        HasWorkingDir = 1 << 4,

        /// <summary>
        /// The shell link is saved with command line arguments. If this bit is set, a
        /// COMMAND_LINE_ARGUMENTS StringData structure (section 2.4) MUST
        /// be present. If this bit is not set, this structure MUST NOT be present.
        /// </summary>
        HasArguments = 1 << 5,

        /// <summary>
        /// The shell link is saved with an icon location string. If this bit is set, an
        /// ICON_LOCATION StringData structure (section 2.4) MUST be present. If
        /// this bit is not set, this structure MUST NOT be present.
        /// </summary>
        HasIconLocation = 1 << 6,

        /// <summary>
        /// The shell link contains Unicode encoded strings. This bit SHOULD be set. If
        /// this bit is set, the StringData section contains Unicode-encoded strings;
        /// otherwise, it contains strings that are encoded using the system default
        /// code page.
        /// </summary>
        IsUnicode = 1 << 7,

        /// <summary>
        /// The LinkInfo structure (section 2.3) is ignored.
        /// </summary>
        ForceNoLinkInfo = 1 << 8,

        /// <summary>
        /// The shell link is saved with an
        /// EnvironmentVariableDataBlock (section 2.5.4).
        /// </summary>
        HasExpString = 1 << 9,

        /// <summary>
        /// The target is run in a separate virtual machine when launching a link
        /// target that is a 16-bit application.
        /// </summary>
        RunInSeparateProcess = 1 << 10,

        /// <summary>
        /// A bit that is undefined and MUST be ignored.
        /// </summary>
        Unused1 = 1 << 11,

        /// <summary>
        /// The shell link is saved with a DarwinDataBlock (section 2.5.3).
        /// </summary>
        HasDarwinID = 1 << 12,

        /// <summary>
        /// The application is run as a different user when the target of the shell link is
        /// activated.
        /// </summary>
        RunAsUser = 1 << 13,

        /// <summary>
        /// The shell link is saved with an IconEnvironmentDataBlock (section 2.5.5).
        /// </summary>
        HasExpIcon = 1 << 14,

        /// <summary>
        /// The file system location is represented in the shell namespace when the 
        /// path to an item is parsed into an IDList.
        /// </summary>
        NoPidlAlias = 1 << 15,

        /// <summary>
        /// A bit that is undefined and MUST be ignored.
        /// </summary>
        Unused2 = 1 << 16,

        /// <summary>
        /// The shell link is saved with a ShimDataBlock (section 2.5.8).
        /// </summary>
        RunWithShimLayer = 1 << 17,

        /// <summary>
        /// The TrackerDataBlock (section 2.5.10) is ignored.
        /// </summary>
        ForceNoLinkTrack = 1 << 18,

        /// <summary>
        /// The shell link attempts to collect target properties and store them in the
        /// PropertyStoreDataBlock (section 2.5.7) when the link target is set.
        /// </summary>
        EnableTargetMetadata = 1 << 19,

        /// <summary>
        /// The EnvironmentVariableDataBlock is ignored.
        /// </summary>
        DisableLinkPathTracking = 1 << 20,

        /// <summary>
        /// The SpecialFolderDataBlock (section 2.5.9) and the
        /// KnownFolderDataBlock (section 2.5.6) are ignored when loading the shell
        /// link. If this bit is set, these extra data blocks SHOULD NOT be saved when
        /// saving the shell link.
        /// </summary>
        DisableKnownFolderTracking = 1 << 21,

        /// <summary>
        /// If the link has a KnownFolderDataBlock (section 2.5.6), the unaliased form
        /// of the known folder IDList SHOULD be used when translating the target
        /// IDList at the time that the link is loaded.
        /// </summary>
        DisableKnownFolderAlias = 1 << 22,

        /// <summary>
        /// Creating a link that references another link is enabled. Otherwise,
        /// specifying a link as the target IDList SHOULD NOT be allowed.
        /// </summary>
        AllowLinkToLink = 1 << 23,

        /// <summary>
        /// When saving a link for which the target IDList is under a known folder,
        /// either the unaliased form of that known folder or the target IDList SHOULD
        /// be used.
        /// </summary>
        UnaliasOnSave = 1 << 24,

        /// <summary>
        /// The target IDList SHOULD NOT be stored; instead, the path specified in the
        /// EnvironmentVariableDataBlock (section 2.5.4) SHOULD be used to refer to
        /// the target.
        /// </summary>
        PreferEnvironmentPath = 1 << 25,

        /// <summary>
        /// When the target is a UNC name that refers to a location on a local
        /// machine, the local path IDList in the
        /// PropertyStoreDataBlock (section 2.5.7) SHOULD be stored, so it can be
        /// used when the link is loaded on the local machine.
        /// </summary>
        KeepLocalIDListForUNCTarget = 1 << 26,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero1 = 1 << 28,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero2 = 1 << 29,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero3 = 1 << 30,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero4 = 1 << 31,
    }
}