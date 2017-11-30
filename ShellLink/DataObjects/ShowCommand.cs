namespace ShellLink
{
    /// <summary>
    /// A 32-bit unsigned integer that specifies the expected window state of an application launched by the link. This value SHOULD be one of the following.
    /// All other values MUST be treated as SW_SHOWNORMAL.
    /// </summary>
    public enum ShowCommand
    {
        /// <summary>
        /// The application is open and its window is open in a normal fashion.
        /// </summary>
        SW_SHOWNORMAL = 0x00000001,

        /// <summary>
        /// The application is open, and keyboard focus is given to the application, but its window is not shown.
        /// </summary>
        SW_SHOWMAXIMIZED = 0x00000003,

        /// <summary>
        /// The application is open, but its window is not shown. It is not given the keyboard focus.
        /// </summary>
        SW_SHOWMINNOACTIVE = 0x00000007,
    }
}