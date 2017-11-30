using System;

namespace ShellLink
{
    [Flags]
    public enum LinkInfoFlags
    {
        /// <summary>
        /// If set, the VolumeID and LocalBasePath fields are present,
        /// and their locations are specified by the values of the
        /// VolumeIDOffset and LocalBasePathOffset fields, respectively.
        /// If the value of the LinkInfoHeaderSize field is greater than
        /// or equal to 0x00000024, the LocalBasePathUnicode field is
        /// present, and its location is specified by the value of the
        /// LocalBasePathOffsetUnicode field.
        /// <para>
        /// If not set, the VolumeID, LocalBasePath, and LocalBasePathUnicode
        /// fields are not present, and the values of the VolumeIDOffset
        /// and LocalBasePathOffset fields are zero. If the value of
        /// the LinkInfoHeaderSize field is greater than or equal to
        /// 0x00000024, the value of the LocalBasePathOffsetUnicode
        /// field is zero.
        /// </para>
        /// </summary>
        VolumeIDAndLocalBasePath = 1 << 0,

        /// <summary>
        /// If set, the CommonNetworkRelativeLink field is present, and its
        /// location is specified by the value of the CommonNetworkRelativeLinkOffset
        /// field.
        /// <para>
        /// If not set, the CommonNetworkRelativeLink field is not present,
        /// and the value of the CommonNetworkRelativeLinkOffset field is zero.
        /// </para>
        /// </summary>
        CommonNetworkRelativeLinkAndPathSuffix = 1 << 1,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zeros = ~3,
    }
}