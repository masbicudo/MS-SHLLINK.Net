using System;
using System.Collections.Generic;

namespace ShellLink.DataObjects
{
    /// <summary>
    /// The stored IDList structure specifies the format of a persisted item ID list.
    /// </summary>
    public sealed class IDList
    {
        /// <summary>
        /// An array of zero or more ItemID structures (section 2.2.2).
        /// </summary>
        public List<ItemID> ItemIDList { get; } = new List<ItemID>();

        /// <summary>
        /// A 16-bit, unsigned integer that indicates the end of the item IDs.
        /// This value MUST be zero.
        /// </summary>
        public UInt16 TerminalID { get; set; }
    }
}