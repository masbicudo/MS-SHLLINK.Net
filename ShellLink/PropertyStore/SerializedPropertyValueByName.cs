namespace ShellLink.PropertyStore
{
    public sealed class SerializedPropertyValueByName : SerializedPropertyValue
    {
        /// <summary>
        /// An unsigned integer that specifies the size, in bytes, of the Name field,
        /// including the null-terminating character.
        /// </summary>
        public int NameSize { get; set; }

        /// <summary>
        /// A null-terminated Unicode string that specifies the identity of the property. It has
        /// to be unique within the enclosing Serialized Property Storage structure.
        /// </summary>
        public string Name { get; set; }
    }
}