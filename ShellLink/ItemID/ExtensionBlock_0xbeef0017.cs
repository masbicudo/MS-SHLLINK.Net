namespace ShellLink.ItemID
{
    public sealed class ExtensionBlock_0xbeef0017 : ExtensionBlock
    {
        /// <summary>
        /// Unknown (Empty values)
        /// </summary>
        public int Unknown1 { get; set; }
        public int Unknown2 { get; set; }
        public int Unknown3 { get; set; }
        public int Unknown4 { get; set; }
        public int Unknown5 { get; set; }
        public int Unknown6 { get; set; }

        /// <summary>
        /// Unknown (Empty values)
        /// 8 bytes
        /// </summary>
        public long Unknown7 { get; set; }
        public int Unknown8 { get; set; }

        /// <summary>
        /// Unknown (Empty values)
        /// 24 bytes
        /// </summary>
        public byte[] Unknown9 { get; set; }

        /// <summary>
        /// First extension block version offset.
        /// The offset is relative from the start of the shell item.
        /// </summary>
        public ushort FirstExtensionBlockVersionOffset { get; set; }
    }
}