namespace ShellLink.DataObjects
{
    /// <summary>
    /// StringData refers to a set of structures that convey user interface
    /// and path identification information. The presence of these optional
    /// structures is controlled by LinkFlags (section 2.1.1) in the
    /// ShellLinkHeader (section 2.1).
    /// The StringData structures conform to the following ABNF rules [RFC5234].
    /// <para>
    /// STRING_DATA = [NAME_STRING] [RELATIVE_PATH] [WORKING_DIR] 
    ///               [COMMAND_LINE_ARGUMENTS] [ICON_LOCATION] 
    /// </para>
    /// </summary>
    public sealed class StringData
    {
        /// <summary>
        ///  An optional structure that specifies a description of the shortcut that is displayed to end users to identify the purpose of the shell link. This structure MUST be present if the HasName flag is set.
        /// </summary>
        public string NameString { get; set; }

        /// <summary>
        /// An optional structure that specifies the location of the link target relative to the file that contains the shell link. When specified, this string SHOULD be used when resolving the link. This structure MUST be present if the HasRelativePath flag is set.
        /// </summary>
        public string RelativePath { get; set; }

        /// <summary>
        /// An optional structure that specifies the file system path of the working directory to be used when activating the link target. This structure MUST be present if the HasWorkingDir flag is set.
        /// </summary>
        public string WorkingDir { get; set; }

        /// <summary>
        /// An optional structure that stores the command-line arguments that are specified when activating the link target. This structure MUST be present if the HasArguments flag is set.
        /// </summary>
        public string CommandLineArguments { get; set; }

        /// <summary>
        /// An optional structure that specifies the location of the icon to be used when displaying a shell link item in an icon view. This structure MUST be present if the HasIconLocation flag is set.
        /// </summary>
        public string IconLocation { get; set; }
    }
}