namespace ShellLink.DataObjects.ExtraData
{
    public abstract class ExtraDataBlock
    {
        /// <summary>
        /// A 16-bit, unsigned integer that specifies the size, in bytes,
        /// of the ItemID structure, including the ItemIDSize field.
        /// </summary>
        public abstract int BlockSize { get; set; }

        public abstract int BlockSignature { get; set; }
    }
}