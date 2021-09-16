using ShellLink.DataObjects.Enums;

namespace ShellLink.DataObjects
{
    /// <summary>
    /// The LinkInfo structure specifies information necessary to resolve a link target
    /// if it is not found in its original location. This includes information about
    /// the volume that the target was stored on, the mapped drive letter,
    /// and a Universal Naming Convention (UNC) form of the path if one existed when
    /// the link was created. For more details about UNC paths,
    /// see [MS-DFSNM] section 2.2.1.4.
    /// </summary>
    public sealed class LinkInfo
    {
        /// <summary>
        /// 32-bit, unsigned integer that specifies the size, in bytes, of the LinkInfo structure. All offsets specified in this structure MUST be less than this value, and all strings contained in this structure MUST fit within the extent defined by this size.
        /// </summary>
        public uint LinkInfoSize { get; set; }

        /// <summary>
        /// Meaning: Offsets to the optional fields are not specified.
        /// </summary>
        public const uint MinLinkInfoHeaderSize = 0x0000001C;

        /// <summary>
        /// Meaning: Offsets to the optional fields are specified.
        /// </summary>
        public const uint MaxLinkInfoHeaderSize = 0x00000024;

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the size, in bytes, of the LinkInfo header section, which is composed of the LinkInfoSize, LinkInfoHeaderSize, LinkInfoFlags, VolumeIDOffset, LocalBasePathOffset, CommonNetworkRelativeLinkOffset, CommonPathSuffixOffset fields, and, if included, the LocalBasePathOffsetUnicode and CommonPathSuffixOffsetUnicode fields.
        /// </summary>
        public uint LinkInfoHeaderSize { get; set; }

        /// <summary>
        /// Flags that specify whether the VolumeID, LocalBasePath, LocalBasePathUnicode, and CommonNetworkRelativeLink fields are present in this structure.
        /// </summary>
        public LinkInfoFlags LinkInfoFlags { get; set; }

        /// <summary>
        /// 32-bit, unsigned integer that specifies the location of the VolumeID field. If the VolumeIDAndLocalBasePath flag is set, this value is an offset, in bytes, from the start of the LinkInfo structure; otherwise, this value MUST be zero.
        /// </summary>
        public uint VolumeIDOffset { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the location of the LocalBasePath field. If the VolumeIDAndLocalBasePath flag is set, this value is an offset, in bytes, from the start of the LinkInfo structure; otherwise, this value MUST be zero.
        /// </summary>
        public uint LocalBasePathOffset { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the location of the CommonNetworkRelativeLink field. If the CommonNetworkRelativeLinkAndPathSuffix flag is set, this value is an offset, in bytes, from the start of the LinkInfo structure; otherwise, this value MUST be zero.
        /// </summary>
        public uint CommonNetworkRelativeLinkOffset { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the location of the CommonPathSuffix field. This value is an offset, in bytes, from the start of the LinkInfo structure.
        /// </summary>
        public uint CommonPathSuffixOffset { get; set; }

        /// <summary>
        /// An optional, 32-bit, unsigned integer that specifies the location of the LocalBasePathUnicode field. If the VolumeIDAndLocalBasePath flag is set, this value is an offset, in bytes, from the start of the LinkInfo structure; otherwise, this value
        /// MUST be zero. This field can be present only if the value of the LinkInfoHeaderSize field is greater than or equal to 0x00000024.
        /// </summary>
        public uint? LocalBasePathOffsetUnicode { get; set; }

        /// <summary>
        /// An optional, 32-bit, unsigned integer that specifies the location of the CommonPathSuffixUnicode field. This value is an offset, in bytes, from the start of the LinkInfo structure. This field can be present only if the value of the LinkInfoHeaderSize field is greater than or equal to 0x00000024.
        /// </summary>
        public uint? CommonPathSuffixOffsetUnicode { get; set; }

        /// <summary>
        /// An optional VolumeID structure (section 2.3.1) that specifies information about the volume that the link target was on when the link was created. This field is present if the VolumeIDAndLocalBasePath flag is set.
        /// </summary>
        public VolumeID VolumeID { get; set; }

        /// <summary>
        /// An optional, NULL–terminated string, defined by the system default code page, which is used to construct the full path to the link item or link target by appending the string in the CommonPathSuffix field. This field is present if the VolumeIDAndLocalBasePath flag is set.
        /// </summary>
        public string LocalBasePath { get; set; }

        /// <summary>
        /// An optional CommonNetworkRelativeLink structure (section 2.3.2) that specifies information about the network location where the link target is stored.
        /// </summary>
        public CommonNetworkRelativeLink CommonNetworkRelativeLink { get; set; }

        /// <summary>
        /// A NULL–terminated string, defined by the system default code page, which is used to construct the full path to the link item or link target by being appended to the string in the LocalBasePath field.
        /// </summary>
        public string CommonPathSuffix { get; set; }

        /// <summary>
        /// An optional, NULL–terminated, Unicode string that is used to construct the full path to the link item or link target by appending the string in the CommonPathSuffixUnicode field. This field can be present only if the VolumeIDAndLocalBasePath flag is set and the value of the LinkInfoHeaderSize field is greater than or equal to 0x00000024.
        /// </summary>
        public string LocalBasePathUnicode { get; set; }

        /// <summary>
        /// An optional, NULL–terminated, Unicode string that is used to construct the full path
        /// to the link item or link target by being appended to the string in the
        /// LocalBasePathUnicode field.
        /// This field can be present only if the value of the
        /// LinkInfoHeaderSize field is greater than or equal to 0x00000024.
        /// </summary>
        public string CommonPathSuffixUnicode { get; set; }
    }
}