using System.IO;
using System.Text;
using JetBrains.Annotations;
using ShellLink.DataObjects;
using ShellLink.Internals;

namespace ShellLink.Loaders
{
    public static class VolumeIDLoader
    {
        public static bool Load([NotNull] this VolumeID obj, BinaryReader reader)
        {
            obj.VolumeIDSize = reader.ReadUInt32();
            obj.DriveType = (DriveType)reader.ReadInt32();
            obj.DriveSerialNumber = reader.ReadUInt32();
            obj.VolumeLabelOffset = reader.ReadUInt32();

            if (obj.VolumeLabelOffset == 0x00000014)
                obj.VolumeLabelOffsetUnicode = reader.ReadUInt32();

            var bytesToEat = (int)(obj.VolumeLabelOffsetUnicode ?? obj.VolumeLabelOffset)
                             - (obj.VolumeLabelOffset != 0x00000014 ? 16 : 20);

            if (bytesToEat > 0)
                reader.ReadBytes(bytesToEat);

            if (obj.VolumeLabelOffsetUnicode == null)
                obj.Data = reader.ReadNullTerminatedString(Encoding.Default);
            else
                obj.Data = reader.ReadNullTerminatedString(Encoding.Unicode);

            return true;
        }
    }
}