using System.IO;
using System.Text;
using ShellLink.DataObjects;
using ShellLink.Internals;

namespace ShellLink.Loaders
{
    public static class LinkInfoLoader
    {
        public static bool Load(this LinkInfo obj, BinaryReader reader)
        {
            var ok = true;

            obj.LinkInfoSize = reader.ReadUInt32();
            obj.LinkInfoHeaderSize = reader.ReadUInt32();
            obj.LinkInfoFlags = (LinkInfoFlags)reader.ReadUInt32();
            obj.VolumeIDOffset = reader.ReadUInt32();
            obj.LocalBasePathOffset = reader.ReadUInt32();
            obj.CommonNetworkRelativeLinkOffset = reader.ReadUInt32();
            obj.CommonPathSuffixOffset = reader.ReadUInt32();

            if (obj.LinkInfoHeaderSize >= 0x00000024)
                obj.LocalBasePathOffsetUnicode = reader.ReadUInt32();

            if (obj.LinkInfoHeaderSize >= 0x00000024)
                obj.CommonPathSuffixOffsetUnicode = reader.ReadUInt32();

            if (obj.LinkInfoFlags.HasFlag(LinkInfoFlags.VolumeIDAndLocalBasePath))
                ok &= obj.VolumeID.Load(reader);

            if (obj.LinkInfoFlags.HasFlag(LinkInfoFlags.VolumeIDAndLocalBasePath))
                obj.LocalBasePath = reader.ReadNullTerminatedString(Encoding.Default);

            if (obj.LinkInfoFlags.HasFlag(LinkInfoFlags.CommonNetworkRelativeLinkAndPathSuffix))
                ok &= obj.CommonNetworkRelativeLink.Load(reader);

            obj.CommonPathSuffix = reader.ReadNullTerminatedString(Encoding.Default);

            if (obj.LinkInfoFlags.HasFlag(LinkInfoFlags.VolumeIDAndLocalBasePath))
                if (obj.LinkInfoHeaderSize >= 0x00000024)
                    obj.LocalBasePathUnicode = reader.ReadNullTerminatedString(Encoding.Unicode);

            if (obj.LinkInfoHeaderSize >= 0x00000024)
                obj.CommonPathSuffixUnicode = reader.ReadNullTerminatedString(Encoding.Unicode);

            return ok;
        }
    }
}