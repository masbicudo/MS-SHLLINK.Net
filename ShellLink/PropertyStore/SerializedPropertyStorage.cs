using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace ShellLink.PropertyStore
{
    /// <summary>
    /// The Serialized Property Storage structure is a sequence of Serialized Property Value structures. The
    /// sequence MUST be terminated by a Serialized Property Value structure that specifies 0x00000000 for
    /// the Value Size field.
    /// </summary>
    public sealed class SerializedPropertyStorage
    {
        /// <summary>
        /// An unsigned integer that specifies the total size, in bytes, of this structure.
        /// It MUST be 0x00000000 if this is the last Serialized Property Storage in the enclosing Serialized
        /// Property Store.
        /// </summary>
        public int StorageSize { get; set; }

        /// <summary>
        /// Has to be equal to 0x53505331.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// A GUID that specifies the semantics and expected usage of the properties
        /// contained in this Serialized Property Storage structure. It MUST be unique in the set of serialized
        /// property storage structures.
        /// </summary>
        public Guid FormatID { get; set; }

        /// <summary>
        /// A sequence of one or more property values. If the Format
        /// ID field is equal to the GUID {D5CDD505-2E9C-101B-9397-08002B2CF9AE}, then all values in
        /// the sequence MUST be Serialized Property Value (String Name) structures, as specified in section
        /// 2.3.1; otherwise, all values MUST be Serialized Property Value (Integer Name) structures, as
        /// specified in section 2.3.2. The last Serialized Property Value in the sequence MUST specify
        /// 0x00000 for the Value Size.
        /// </summary>
        // TODO: while reading items, must try both types, first the Named type, then the Id type
        // TODO: the named type should only be allowed to be, if the length matches the position of
        // TODO: zero char terminator '\0', otherwise, it must be considered Id type.
        // TODO: SEE SerializedPropertyValue_ByName
        // TODO: SEE SerializedPropertyValue_ById
        [NotNull]
        public List<SerializedPropertyValue> SerializedPropertyValues { get; } = new List<SerializedPropertyValue>();
    }
}