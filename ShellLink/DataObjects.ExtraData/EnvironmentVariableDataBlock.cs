namespace ShellLink.DataObjects.ExtraData
{
    /// <summary>
    /// The EnvironmentVariableDataBlock structure specifies
    /// a path to environment variable information when the
    /// link target refers to a location that has a corresponding
    /// environment variable.
    /// </summary>
    public sealed class EnvironmentVariableDataBlock : ExtraDataBlock
    {
        /// <summary>
        /// A 32-bit, unsigned integer that specifies the
        /// size of the EnvironmentVariableDataBlock structure.
        /// This value MUST be 0x00000314.
        /// </summary>
        public override uint BlockSize { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the signature
        /// of the EnvironmentVariableDataBlock extra data section.
        /// This value MUST be 0xA0000001.
        /// </summary>
        public override int BlockSignature { get; set; }

        /// <summary>
        /// A NULL-terminated string, defined by the syste
        ///  default code page, which specifies a path t
        ///  environment variable information.
        /// </summary>
        public string TargetAnsi { get; set; }

        /// <summary>
        /// An optional, NULL-terminated, Unicode string that
        /// specifies a path to environment variable information.
        /// </summary>
        public string TargetUnicode { get; set; }
    }
}