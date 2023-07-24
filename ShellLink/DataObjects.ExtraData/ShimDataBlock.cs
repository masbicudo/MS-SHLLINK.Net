namespace ShellLink.DataObjects.ExtraData
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
        public override uint BlockSize { get; set; }

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
    }
}