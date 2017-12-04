using System.Collections.Generic;

namespace ShellLink.ExtraData
{
    /// <summary>
    /// ExtraData refers to a set of structures that convey additional
    /// information about a link target. These optional structures can
    /// be present in an extra data section that is appended to the
    /// basic Shell Link Binary File Format.
    /// 
    /// The ExtraData structures conform to the following ABNF rules[RFC5234]: 
    /// <para>
    /// EXTRA_DATA = *EXTRA_DATA_BLOCK TERMINAL_BLOCK
    /// </para>
    /// <para>
    /// EXTRA_DATA_BLOCK = CONSOLE_PROPS / CONSOLE_FE_PROPS / DARWIN_PROPS /
    ///                    ENVIRONMENT_PROPS / ICON_ENVIRONMENT_PROPS /
    ///                    KNOWN_FOLDER_PROPS / PROPERTY_STORE_PROPS /
    ///                    SHIM_PROPS / SPECIAL_FOLDER_PROPS /
    ///                    TRACKER_PROPS / VISTA_AND_ABOVE_IDLIST_PROPS
    /// </para>
    /// </summary>
    public sealed class ExtraData
    {
        /// <summary>
        /// An optional array of bytes that contains zero or more property
        /// data blocks listed in the EXTRA_DATA_BLOCK syntax rule.
        /// </summary>
        public List<ExtraDataBlock> ExtraDataBlock { get; } = new List<ExtraDataBlock>();

        /// <summary>
        /// A 32-bit, unsigned integer that indicates the end of the item IDs.
        /// This value MUST be &lt; 4.
        /// </summary>
        public int TerminalID { get; set; }
    }
}