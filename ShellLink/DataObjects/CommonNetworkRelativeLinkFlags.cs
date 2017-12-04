using System;

namespace ShellLink.DataObjects
{
    [Flags]
    public enum CommonNetworkRelativeLinkFlags
    {
        /// <summary>
        /// If set, the DeviceNameOffset field contains an offset to the device name.
        /// <para>
        /// If not set, the DeviceNameOffset field does not contain an offset to the device name, and its value MUST be zero.
        /// </para>
        /// </summary>
        ValidDevice = 1 << 0,

        /// <summary>
        /// If set, the NetProviderType field contains the network provider type.
        /// <para>
        /// If not set, the NetProviderType field does not contain the network provider type, and its value MUST be zero.
        /// </para>
        /// </summary>
        ValidNetType = 1 << 1,

        /// <summary>
        /// Must be zero.
        /// </summary>
        Zeros = ~3,
    }
}