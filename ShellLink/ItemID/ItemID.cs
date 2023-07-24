namespace ShellLink.ItemID
{
    /// <summary>
    /// An ItemID is an element in an IDList structure (section 2.2.1).
    /// The data stored in a given ItemID is defined by the source that
    /// corresponds to the location in the target namespace of the
    /// preceding ItemIDs. This data uniquely identifies the items in
    /// that part of the namespace.
    /// </summary>
    public abstract class ShellItemId
    {
        /// <summary>
        /// A 16-bit, unsigned integer that specifies the size, in bytes,
        /// of the ItemID structure, including the ItemIDSize field.
        /// </summary>
        public ushort ItemIDSize { get; set; }
    }
}