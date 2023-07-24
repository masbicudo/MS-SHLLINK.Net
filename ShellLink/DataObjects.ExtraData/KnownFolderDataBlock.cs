using System;

namespace ShellLink.DataObjects.ExtraData
{
    /// <summary>
    /// The KnownFolderDataBlock structure specifies the location of
    /// a known folder. This data can be used when a link target is
    /// a known folder to keep track of the folder so that the link
    /// target IDList can be translated when the link is loaded.
    /// </summary>
    public sealed class KnownFolderDataBlock : ExtraDataBlock
    {
        /// <summary>
        /// A 32-bit, unsigned integer that specifies the size of
        /// the KnownFolderDataBlock structure.
        /// This value MUST be 0x0000001C.
        /// </summary>
        public override uint BlockSize { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the signature
        /// of the KnownFolderDataBlock extra data section.
        /// This value MUST be 0xA000000B.
        /// </summary>
        public override int BlockSignature { get; set; }

        /// <summary>
        /// A value in GUID packet representation
        /// ([MS-DTYP] section 2.3.2.2) that specifies the folder GUID ID.
        /// </summary>
        public Guid KnownFolderID { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the location of
        /// the ItemID of the first child segment of the IDList specified by
        /// KnownFolderID. This value is the offset, in bytes,
        /// into the link target IDList.
        /// </summary>
        public int Offset { get; set; }
    }
}