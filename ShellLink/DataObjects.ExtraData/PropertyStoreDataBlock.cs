using JetBrains.Annotations;
using ShellLink.PropertyStore;

namespace ShellLink.DataObjects.ExtraData
{
    /// <summary>
    /// A PropertyStoreDataBlock structure specifies a set of properties
    /// that can be used by applications to store extra data in the shell link.
    /// </summary>
    public sealed class PropertyStoreDataBlock : ExtraDataBlock
    {
        /// <summary>
        /// A 32-bit, unsigned integer that specifies the size of the
        /// PropertyStoreDataBlock structure.
        /// This value MUST be greater than or equal to 0x0000000C.
        /// </summary>
        public override uint BlockSize { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the signature of the
        /// PropertyStoreDataBlock extra data section.
        /// This value MUST be 0xA0000009.
        /// </summary>
        public override int BlockSignature { get; set; }

        /// <summary>
        /// A serialized property storage structure ([MS-PROPSTORE] section 2.2).
        /// </summary>
        [NotNull]
        public SerializedPropertyStore PropertyStore { get; } = new SerializedPropertyStore();
    }
}