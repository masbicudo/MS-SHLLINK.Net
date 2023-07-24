using ShellLink.DataObjects;

namespace ShellLink.DataObjects.ExtraData
{
    /// <summary>
    /// The IconEnvironmentDataBlock structure specifies
    /// the path to an icon. The path is encoded using
    /// environment variables, which makes it possible
    /// to find the icon across machines where the
    /// locations vary but are expressed using
    /// environment variables.
    /// </summary>
    public sealed class IconEnvironmentDataBlock : ExtraDataBlock
    {
        /// <summary>
        /// A 32-bit, unsigned integer that specifies
        /// the size of the IconEnvironmentDataBlock
        /// structure. This value MUST be 0x00000314.
        /// </summary>
        public override uint BlockSize { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the
        /// signature of the IconEnvironmentDataBlock
        /// extra data section. This value MUST be 0xA0000007.
        /// </summary>
        public override int BlockSignature { get; set; }

        /// <summary>
        /// A NULL-terminated string, defined by the
        /// system default code page, which specifies
        /// a path that is constructed with
        /// environment variables.
        /// </summary>
        public string TargetAnsi { get; set; }

        /// <summary>
        /// An optional, NULL-terminated, Unicode string
        /// that specifies a path that is constructed with
        /// environment variables.
        /// </summary>
        public string TargetUnicode { get; set; }
    }
}