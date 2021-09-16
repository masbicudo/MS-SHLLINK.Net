using JetBrains.Annotations;
using System.Collections.Generic;

namespace ShellLink.PropertyStore
{
    /// <summary>
    /// The Property Store Binary File Format is a sequence of Serialized Property Storage structures. The
    /// sequence MUST be terminated by a Serialized Property Storage structure that specifies 0x00000000
    /// for the Storage Size field.
    /// </summary>
    public sealed class SerializedPropertyStore
    {
        /// <summary>
        /// An unsigned integer that specifies the total size, in bytes, of this structure,
        /// excluding the size of this field.
        /// </summary>
        public int StoreSize { get; set; }

        /// <summary>
        /// A sequence of one or more Serialized Property Storage
        /// structures, as specified in section 2.2.
        /// </summary>
        [NotNull]
        public List<SerializedPropertyStorage> SerializedPropertyStorages { get; } = new List<SerializedPropertyStorage>();

    }
}