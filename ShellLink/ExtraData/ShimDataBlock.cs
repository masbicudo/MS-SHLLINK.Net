using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ShellLink.DataObjects;
using ShellLink.Internals;

namespace ShellLink.ExtraData
{
    /// <summary>
    /// The ShimDataBlock structure specifies
    /// the name of a shim that can be applied
    /// when activating a link target.
    /// </summary>
    public sealed class ShimDataBlock : ExtraDataBlock
    {
        /// <summary>
        /// A 32-bit, unsigned integer that specifies
        /// the size of the ShimDataBlock structure.
        /// This value MUST be greater than or equal to 0x00000088.
        /// </summary>
        public override int BlockSize { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies
        /// the signature of the ShimDataBlock extra data section.
        /// This value MUST be 0xA0000008.
        /// </summary>
        public override int BlockSignature { get; set; }

        /// <summary>
        /// A Unicode string that specifies
        /// the name of a shim layer to apply to
        /// a link target when it is being activated.
        /// </summary>
        public string LayerName { get; set; }

        protected override int GetDataLength() => 0x00000088 - ExtraDataBlock.SizeAndSigFieldLength;

        public override int GetSignatureValue() => unchecked((int)0xA0000008);

        protected override void WriteDataTo(BinaryWriter writer, IOptions options)
        {
            writer.WriteNullTerminatedString(this.LayerName, Encoding.Unicode, minSize: 0x80);
        }

        protected override bool LoadData(BinaryReader reader, IOptions options)
        {
            this.LayerName = reader.ReadNullTerminatedString(Encoding.Unicode);
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