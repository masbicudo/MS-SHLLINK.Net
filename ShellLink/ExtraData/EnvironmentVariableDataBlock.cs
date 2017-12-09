using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ShellLink.DataObjects;
using ShellLink.Internals;

namespace ShellLink.ExtraData
{
    /// <summary>
    /// The EnvironmentVariableDataBlock structure specifies
    /// a path to environment variable information when the
    /// link target refers to a location that has a corresponding
    /// environment variable.
    /// </summary>
    public sealed class EnvironmentVariableDataBlock : ExtraDataBlock
    {
        /// <summary>
        /// A 32-bit, unsigned integer that specifies the
        /// size of the EnvironmentVariableDataBlock structure.
        /// This value MUST be 0x00000314.
        /// </summary>
        public override int BlockSize { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the signature
        /// of the EnvironmentVariableDataBlock extra data section.
        /// This value MUST be 0xA0000001.
        /// </summary>
        public override int BlockSignature { get; set; }

        /// <summary>
        /// A NULL-terminated string, defined by the syste
        ///  default code page, which specifies a path t
        ///  environment variable information.
        /// </summary>
        public string TargetAnsi { get; set; }

        /// <summary>
        /// An optional, NULL-terminated, Unicode string that
        /// specifies a path to environment variable information.
        /// </summary>
        public string TargetUnicode { get; set; }

        protected override int GetDataLength() => 0x00000314 - ExtraDataBlock.SizeAndSigFieldLength;

        public override int GetSignatureValue() => unchecked((int)0xA0000001);

        protected override void WriteDataTo(BinaryWriter writer)
        {
            writer.WriteFixedSizeString(this.TargetAnsi, 260, Encoding.Default);
            writer.WriteFixedSizeString(this.TargetUnicode, 520, Encoding.Unicode);
        }

        protected override bool LoadData(BinaryReader reader)
        {
            this.TargetAnsi = reader.ReadFixedSizeString(260, Encoding.Default, ZeroCharBehavior.RemoveTrailing);
            this.TargetUnicode = reader.ReadFixedSizeString(520, Encoding.Unicode, ZeroCharBehavior.RemoveTrailing);
            return true;
        }

        protected override void CheckData(List<Exception> errors, ShellLinkObject shellLinkObject)
        {
            ExtraDataBlock.CheckString(errors, this.TargetAnsi, 260, nameof(TargetAnsi));
            ExtraDataBlock.CheckString(errors, this.TargetUnicode, 260, nameof(TargetUnicode));
        }

        protected override void RepairData()
        {
            // TODO: this should be the same for:
            // - DarwinDataBlock
            // - EnvironmentVariableDataBlock
            // - IconEnvironmentDataBlock
        }
    }
}