using ShellLink.DataObjects;

namespace ShellLink.DataObjects.ExtraData
{
    /// <summary>
    /// The VistaAndAboveIDListDataBlock structure specifies
    /// an alternate IDList that can be used instead of the
    /// LinkTargetIDList structure (section 2.2) on platforms that support it.<5>
    /// </summary>
    public sealed class VistaAndAboveIDListDataBlock : ExtraDataBlock
    {
        /// <summary>
        /// A 32-bit, unsigned integer that specifies
        /// the size of the VistaAndAboveIDListDataBlock structure.
        /// This value MUST be greater than or equal to 0x0000000A.
        /// </summary>
        public override int BlockSize { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies
        /// the signature of the VistaAndAboveIDListDataBlock extra data section.
        /// This value MUST be 0xA000000C.
        /// </summary>
        public override int BlockSignature { get; set; }

        /// <summary>
        /// An IDList structure (section 2.2.1).
        /// </summary>
        public IDList IDList { get; } = new IDList();
    }
}