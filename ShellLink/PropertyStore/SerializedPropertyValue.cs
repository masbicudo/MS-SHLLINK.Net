namespace ShellLink.PropertyStore
{
    public abstract class SerializedPropertyValue
    {
        /// <summary>
        /// An unsigned integer that specifies the total size, in bytes, of this structure. It
        /// MUST be 0x00000000 if this is the last The Serialized Property Value in the enclosing Serialized
        /// Property Storage structure.
        /// </summary>
        public int ValueSize { get; set; }

        /// <summary>
        /// Has to be 0x00.
        /// </summary>
        public byte Reserved { get; set; }

        /// <summary>
        /// A TypedPropertyValue structure, as specified in [MS-OLEPS] section 2.15.
        /// </summary>
        public byte[] Value { get; set; }
    }
}