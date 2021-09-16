using System;

namespace ShellLink.DataObjects.Enums
{
    /// <summary>
    /// The FileAttributesFlags structure defines bits that specify the file attributes of the link target, if the
    /// target is a file system item. File attributes can be used if the link target is not available, or if accessing
    /// the target would be inefficient. It is possible for the target items attributes to be out of sync with this
    /// value.
    /// </summary>
    [Flags]
    public enum FileAttributes
    {
        /// <summary>
        /// The file or directory is read-only. For a file, if this bit is set,
        /// applications can read the file but cannot write to it or delete
        /// it. For a directory, if this bit is set, applications cannot delete
        /// the directory.
        /// </summary>
        FILE_ATTRIBUTE_READONLY = 1 << 0,

        /// <summary>
        /// The file or directory is hidden. If this bit is set, the file or
        /// folder is not included in an ordinary directory listing.
        /// </summary>
        FILE_ATTRIBUTE_HIDDEN = 1 << 1,

        /// <summary>
        /// The file or directory is part of the operating system or is used
        /// exclusively by the operating system.
        /// </summary>
        FILE_ATTRIBUTE_SYSTEM = 1 << 2,

        /// <summary>
        /// A bit that MUST be zero.
        /// </summary>
        Reserved1 = 1 << 3,

        /// <summary>
        /// The link target is a directory instead of a file.
        /// </summary>
        FILE_ATTRIBUTE_DIRECTORY = 1 << 4,

        /// <summary>
        /// The file or directory is an archive file. Applications use this
        /// flag to mark files for backup or removal.
        /// </summary>
        FILE_ATTRIBUTE_ARCHIVE = 1 << 5,

        /// <summary>
        /// A bit that MUST be zero.
        /// </summary>
        Reserved2 = 1 << 6,

        /// <summary>
        /// The file or directory has no other flags set. If this bit is 1, all
        /// other bits in this structure MUST be clear.
        /// </summary>
        FILE_ATTRIBUTE_NORMAL = 1 << 7,

        /// <summary>
        /// The file is being used for temporary storage.
        /// </summary>
        FILE_ATTRIBUTE_TEMPORARY = 1 << 8,

        /// <summary>
        /// The file is a sparse file.
        /// </summary>
        FILE_ATTRIBUTE_SPARSE_FILE = 1 << 9,

        /// <summary>
        /// The file or directory has an associated reparse point.
        /// </summary>
        FILE_ATTRIBUTE_REPARSE_POINT = 1 << 10,

        /// <summary>
        /// The file or directory is compressed. For a file, this means that
        /// all data in the file is compressed. For a directory, this means
        /// that compression is the default for newly created files and
        /// subdirectories.
        /// </summary>
        FILE_ATTRIBUTE_COMPRESSED = 1 << 11,

        /// <summary>
        /// The data of the file is not immediately available.
        /// </summary>
        FILE_ATTRIBUTE_OFFLINE = 1 << 12,

        /// <summary>
        /// The contents of the file need to be indexed.
        /// </summary>
        FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 1 << 13,

        /// <summary>
        /// The file or directory is encrypted. For a file, this means that
        /// all data in the file is encrypted. For a directory, this means
        /// that encryption is the default for newly created files and
        /// subdirectories.
        /// </summary>
        FILE_ATTRIBUTE_ENCRYPTED = 1 << 14,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero1 = 1 << 15,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero2 = 1 << 16,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero3 = 1 << 17,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero4 = 1 << 18,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero5 = 1 << 19,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero6 = 1 << 20,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero7 = 1 << 21,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero8 = 1 << 22,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero9 = 1 << 23,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero10 = 1 << 24,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero11 = 1 << 25,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero12 = 1 << 26,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero13 = 1 << 27,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero14 = 1 << 28,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero15 = 1 << 29,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero16 = 1 << 30,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zero17 = 1 << 31,
    }
}