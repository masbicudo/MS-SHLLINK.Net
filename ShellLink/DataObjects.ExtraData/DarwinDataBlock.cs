namespace ShellLink.DataObjects.ExtraData
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
    }
}