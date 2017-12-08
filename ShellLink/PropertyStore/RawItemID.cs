namespace ShellLink.PropertyStore
{
    public sealed class SerializedPropertyValueById : SerializedPropertyValue
    {
        /// <summary>
        /// An unsigned integer that specifies the identity of the property. It MUST be unique
        /// within the enclosing Serialized Property Storage structure.
        /// </summary>
        public int Id { get; set; }
    }
}