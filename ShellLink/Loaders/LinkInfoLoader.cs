using JetBrains.Annotations;
using ShellLink.DataObjects;
using ShellLink.DataObjects.Enums;
using ShellLink.Internals;
using System.IO;
using System.Text;

namespace ShellLink.Loaders
{
    public static class LinkInfoLoader
    {
        public static bool Load([NotNull] this LinkInfo obj, BinaryReader reader)
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
            {
                obj.VolumeID = obj.VolumeID ?? new VolumeID();
                ok &= obj.VolumeID.Load(reader);
            }
            else
            {
                obj.VolumeID = null;
            }

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