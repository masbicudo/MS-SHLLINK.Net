using System;
using ShellLink.DataObjects;

namespace ShellLink
{
    /// <summary>
    /// The LinkTargetIDList structure specifies the target of the link.
    /// The presence of this optional structure is specified by the
    /// HasLinkTargetIDList bit (LinkFlags section 2.1.1) in the
    /// ShellLinkHeader (section 2.1).
    /// </summary>
    public sealed class LinkTargetIDList
    {
        /// <summary>
        /// The size, in bytes, of the IDList field.
        /// </summary>
        public UInt16 IDListSize { get; set; }

        /// <summary>
        /// A stored IDList structure (section 2.2.1), which contains the item
        /// ID list. An IDList structure conforms to the following ABNF [RFC5234]:
        /// IDLIST = *ITEMID TERMINALID
        /// </summary>
        public IDList IDList { get; } = new IDList();
    }
}