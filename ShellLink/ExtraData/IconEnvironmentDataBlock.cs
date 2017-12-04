using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ShellLink.Internals;

namespace ShellLink.ExtraData
{
    /// <summary>
    /// The IconEnvironmentDataBlock structure specifies
    /// the path to an icon. The path is encoded using
    /// environment variables, which makes it possible
    /// to find the icon across machines where the
    /// locations vary but are expressed using
    /// environment variables.
    /// </summary>
    public sealed class IconEnvironmentDataBlock : ExtraDataBlock
    {
        /// <summary>
        /// A 32-bit, unsigned integer that specifies
        /// the size of the IconEnvironmentDataBlock
        /// structure. This value MUST be 0x00000314.
        /// </summary>
        public override int BlockSize { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the
        /// signature of the IconEnvironmentDataBlock
        /// extra data section. This value MUST be 0xA0000007.
        /// </summary>
        public override int BlockSignature { get; set; }

        /// <summary>
        /// A NULL-terminated string, defined by the
        /// system default code page, which specifies
        /// a path that is constructed with
        /// environment variables.
        /// </summary>
        public string TargetAnsi { get; set; }

        /// <summary>
        /// An optional, NULL-terminated, Unicode string
        /// that specifies a path that is constructed with
        /// environment variables.
        /// </summary>
        public string TargetUnicode { get; set; }

        protected override int GetDataLength() => 0x00000314 - ExtraDataBlock.SizeAndSigFieldLength;

        public override int GetSignatureValue() => unchecked((int)0xA0000007);

        protected override void WriteDataTo(BinaryWriter writer)
        {
            writer.WriteFixedSizeString(this.TargetAnsi, 260, Encoding.Default);
            writer.WriteFixedSizeString(this.TargetUnicode, 520, Encoding.Unicode);
        }

        protected override bool LoadData(BinaryReader reader)
        {
            this.TargetAnsi = reader.ReadFixedSizeString(260, Encoding.Default);
            this.TargetUnicode = reader.ReadFixedSizeString(520, Encoding.Unicode);
            return true;
        }

        protected override void CheckData(List<Exception> errors)
        {
            CheckString(errors, this.TargetAnsi, 260, nameof(TargetAnsi));
            CheckString(errors, this.TargetUnicode, 260, nameof(TargetUnicode));
        }

        protected override void RepairData()
        {
            // TODO
        }
    }
}