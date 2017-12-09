using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ShellLink.DataObjects;
using ShellLink.Internals;

namespace ShellLink.ExtraData
{
    /// <summary>
    /// The DarwinDataBlock structure specifies an application
    /// identifier that can be used instead of a link target
    /// IDList to install an application when a shell link
    /// is activated.
    /// </summary>
    public sealed class DarwinDataBlock : ExtraDataBlock
    {
        /// <summary>
        /// 32-bit, unsigned integer that specifies the
        /// size of the DarwinDataBlock structure.
        /// This value MUST be 0x00000314.
        /// </summary>
        public override int BlockSize { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the
        /// signature of the DarwinDataBlock extra data section.
        /// This value MUST be 0xA0000006.
        /// </summary>
        public override int BlockSignature { get; set; }

        /// <summary>
        /// NULL–terminated string, defined by the
        /// system default code page, which specifies
        /// an application identifier.
        /// This field SHOULD be ignored.
        /// </summary>
        public string DarwinDataAnsi { get; set; }

        /// <summary>
        /// An optional, NULL–terminated, Unicode string
        /// that specifies an application identifier.
        /// <para>
        /// In Windows, this is a Windows Installer (MSI)
        /// application descriptor. For more information,
        /// see [MSDN-MSISHORTCUTS].
        /// </para>
        /// </summary>
        public string DarwinDataUnicode { get; set; }

        protected override int GetDataLength() => 0x00000314 - ExtraDataBlock.SizeAndSigFieldLength;

        public override int GetSignatureValue() => unchecked((int)0xA0000006);

        protected override void WriteDataTo(BinaryWriter writer)
        {
            writer.WriteFixedSizeString(this.DarwinDataAnsi, 260, Encoding.Default);
            writer.WriteFixedSizeString(this.DarwinDataUnicode, 520, Encoding.Unicode);
        }

        protected override bool LoadData(BinaryReader reader)
        {
            this.DarwinDataAnsi = reader.ReadFixedSizeString(260, Encoding.Default, ZeroCharBehavior.RemoveTrailing);
            this.DarwinDataUnicode = reader.ReadFixedSizeString(520, Encoding.Unicode, ZeroCharBehavior.RemoveTrailing);
            return true;
        }

        protected override void CheckData(List<Exception> errors, ShellLinkObject shellLinkObject)
        {
            CheckString(errors, this.DarwinDataAnsi, 260, nameof(DarwinDataAnsi));
            CheckString(errors, this.DarwinDataUnicode, 260, nameof(DarwinDataUnicode));
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