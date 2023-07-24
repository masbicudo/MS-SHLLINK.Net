namespace ShellLink.DataObjects.ExtraData
{
    /// <summary>
    /// The ConsoleFEDataBlock structure specifies the
    /// code page to use for displaying text when a
    /// link target specifies an application that is
    /// run in a console window.
    /// </summary>
    public sealed class ConsoleFEDataBlock : ExtraDataBlock
    {
        /// <summary>
        /// A 32-bit, unsigned integer that specifies
        /// the size of the ConsoleFEDataBlock structure.
        /// This value MUST be 0x0000000C.
        /// </summary>
        public override uint BlockSize { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies
        /// the signature of the ConsoleFEDataBlock
        /// extra data section. This value MUST be 0xA0000004.
        /// </summary>
        public override int BlockSignature { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies a
        /// code page language code identifier.
        /// For details concerning the structure and meaning
        /// of language code identifiers, see [MS-LCID].
        /// For additional background information,
        /// see [MSCHARSET] and [MSDN-CODEPAGE].
        /// </summary>
        public int CodePage { get; set; }
    }
}