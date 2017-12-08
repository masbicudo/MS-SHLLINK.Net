using System.Collections.Generic;

namespace ShellLink.PropertyStore
{
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
        public List<SerializedPropertyStorage> SerializedPropertyStorages { get; set; }

    }
}