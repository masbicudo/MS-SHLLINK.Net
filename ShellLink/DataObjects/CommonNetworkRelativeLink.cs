namespace ShellLink.DataObjects
{
    public sealed class CommonNetworkRelativeLink
    {
        /// <summary>
        /// A 32-bit, unsigned integer that specifies the size, in bytes, of the
        /// CommonNetworkRelativeLink structure. This value MUST be greater than
        /// or equal to 0x00000014. All offsets specified in this structure
        /// MUST be less than this value, and all strings contained in this
        /// structure MUST fit within the extent defined by this size.
        /// </summary>
        public uint CommonNetworkRelativeLinkSize { get; set; }

        /// <summary>
        /// Flags that specify the contents of the DeviceNameOffset and
        /// NetProviderType fields.
        /// </summary>
        public CommonNetworkRelativeLinkFlags CommonNetworkRelativeLinkFlags { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the location of the
        /// NetName field. This value is an offset, in bytes, from the start
        /// of the CommonNetworkRelativeLink structure.
        /// </summary>
        public uint NetNameOffset { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the location of the
        /// DeviceName field. If the ValidDevice flag is set, this value is an
        /// offset, in bytes, from the start of the CommonNetworkRelativeLink
        /// structure; otherwise, this value MUST be zero.
        /// </summary>
        public uint DeviceNameOffset { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the type of network provider.
        /// If the ValidNetType flag is set, this value MUST be one of the following;
        /// otherwise, this value MUST be ignored.
        /// </summary>
        public NetworkProviderType NetworkProviderType { get; set; }

        /// <summary>
        /// An optional, 32-bit, unsigned integer that specifies the location of
        /// the NetNameUnicode field. This value is an offset, in bytes, from the
        /// start of the CommonNetworkRelativeLink structure. This field MUST be
        /// present if the value of the NetNameOffset field is greater than 0x00000014;
        /// otherwise, this field MUST NOT be present.
        /// </summary>
        public uint? NetNameOffsetUnicode { get; set; }

        /// <summary>
        /// An optional, 32-bit, unsigned integer that specifies the location of the
        /// DeviceNameUnicode field. This value is an offset, in bytes, from the start
        /// of the CommonNetworkRelativeLink structure. This field MUST be present if
        /// the value of the NetNameOffset field is greater than 0x00000014;
        /// otherwise, this field MUST NOT be present.
        /// </summary>
        public uint? DeviceNameOffsetUnicode { get; set; }

        /// <summary>
        /// A NULL–terminated string, as defined by the system default code page,
        /// which specifies a server share path; for example, "\\server\share".
        /// </summary>
        public string NetName { get; set; }

        /// <summary>
        /// A NULL–terminated string, as defined by the system default code page,
        /// which specifies a device; for example, the drive letter "D:".
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// An optional, NULL–terminated, Unicode string that is the Unicode version
        /// of the NetName string. This field MUST be present if the value of the
        /// NetNameOffset field is greater than 0x00000014; otherwise, this field
        /// MUST NOT be present.
        /// </summary>
        public string NetNameUnicode { get; set; }

        /// <summary>
        /// An optional, NULL–terminated, Unicode string that is the Unicode version
        /// of the DeviceName string. This field MUST be present if the value of the
        /// NetNameOffset field is greater than 0x00000014; otherwise, this field
        /// MUST NOT be present.
        /// </summary>
        public string DeviceNameUnicode { get; set; }
    }
}