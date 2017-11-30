using JetBrains.Annotations;

namespace ShellLink.DataObjects
{
    /// <summary>
    /// The Shell Link Binary File Format consists of a sequence of structures that
    /// conform to the following ABNF rules [RFC5234].
    /// <para>
    /// SHELL_LINK = SHELL_LINK_HEADER [LINKTARGET_IDLIST] [LINKINFO] [STRING_DATA] *EXTRA_DATA
    /// </para>
    /// </summary>
    public sealed class ShellLinkObject
    {
        /// <summary>
        /// A ShellLinkHeader structure (section 2.1), which contains
        /// identification information, timestamps, and flags that
        /// specify the presence of optional structures.
        /// </summary>
        [NotNull]
        public ShellLinkHeader ShellLinkHeader { get; } = new ShellLinkHeader();

        /// <summary>
        /// An optional LinkTargetIDList structure (section 2.2),
        /// which specifies the target of the link. The presence of
        /// this structure is specified by the HasLinkTargetIDList
        /// bit (LinkFlags section 2.1.1) in the ShellLinkHeader.
        /// </summary>
        [CanBeNull]
        public LinkTargetIDList LinkTargetIDList { get; set; }

        [CanBeNull]
        public LinkInfo LinkInfo { get; set; }

        [NotNull]
        public StringData StringData { get; } = new StringData();

        //public ExtraData ExtraData { get; set; }
    }
}
