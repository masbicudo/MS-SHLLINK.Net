using System.Collections.Generic;

namespace ShellLink.PropertyStore
{
    /// <summary>
    /// The Property Store Binary File Format is a sequence of Serialized Property Storage structures. The
    /// sequence MUST be terminated by a Serialized Property Storage structure that specifies 0x00000000
    /// for the Storage Size field.
    /// </summary>
    public sealed class PropertyStore
    {
        public List<SerializedPropertyStore> PropertyStores { get; set; }
    }
}