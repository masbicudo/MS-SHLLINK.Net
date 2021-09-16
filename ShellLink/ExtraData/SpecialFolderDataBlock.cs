using System;
using System.Collections.Generic;
using System.IO;
using ShellLink.DataObjects;
using ShellLink.Internals;

namespace ShellLink.ExtraData
{
    /// <summary>
    /// The SpecialFolderDataBlock structure specifies the location
    /// of a special folder. This data can be used when a link target
    /// is a special folder to keep track of the folder, so that the
    /// link target IDList can be translated when the link is loaded.
    /// </summary>
    public sealed class SpecialFolderDataBlock : ExtraDataBlock
    {
        /// <summary>
        /// A 32-bit, unsigned integer that specifies
        /// the size of the SpecialFolderDataBlock structure.
        /// This value MUST be 0x00000010.
        /// </summary>
        public override int BlockSize { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies
        /// the signature of the SpecialFolderDataBlock extra data section.
        /// This value MUST be 0xA0000005.
        /// </summary>
        public override int BlockSignature { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the folder integer ID.
        /// <br/>
        /// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-shllink/ac7b5968-68f5-4e5c-8b11-00a6f4ae98ff#gt_04363af6-0808-468c-92c1-7fa3f57ff76c">
        /// folder integer ID
        /// </seealso>: An integer value that identifies a known folder.
        /// </summary>
        public int SpecialFolderID { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the location of the ItemID of the first child segment of the IDList specified by SpecialFolderID. This value is the offset, in bytes, into the link target IDList.
        /// </summary>
        public int Offset { get; set; }

        protected override int GetDataLength() => 0x00000010 - ExtraDataBlock.SizeAndSigFieldLength;

        public override int GetSignatureValue() => unchecked((int)0xA0000005);

        protected override void WriteDataTo(BinaryWriter writer, IOptions options)
        {
            writer.Write((int)this.SpecialFolderID);
            writer.Write((int)this.Offset);
        }

        protected override bool LoadData(BinaryReader reader, IOptions options)
        {
            this.SpecialFolderID = reader.ReadInt32();
            this.Offset = reader.ReadInt32();
            return true;
        }

        protected override void CheckData(List<Exception> errors, ShellLinkObject shellLinkObject)
        {
        }

        protected override void RepairData()
        {
            // TODO
        }
    }
}