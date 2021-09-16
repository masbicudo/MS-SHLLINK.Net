using JetBrains.Annotations;
using ShellLink.Internals;
using ShellLink.PropertyStore;
using System.IO;
using System.Text;

namespace ShellLink.Actuators.ExtraData
{
    public static class SerializedPropertyValueLoader
    {
        public static bool Load([NotNull] this SerializedPropertyValueByName obj, BinaryReader reader)
        {
            obj.ValueSize = reader.ReadInt32();

            if (obj.ValueSize == 0)
                return true;

            obj.NameSize = reader.ReadInt32();

            obj.Reserved = reader.ReadByte();

            var name = reader.ReadFixedSizeString(obj.NameSize, Encoding.Unicode, ZeroCharBehavior.None);
            var hasZeroAtEndOnly = name.IndexOf('\0') == name.Length - 1;
            obj.Name = name.TrimEnd('\0');

            var remainingBytes = obj.ValueSize - 4 - 4 - 1 - obj.NameSize;
            obj.Value = reader.ReadBytes(remainingBytes);

            return hasZeroAtEndOnly;
        }

        public static bool Load([NotNull] this SerializedPropertyValueById obj, BinaryReader reader)
        {
            obj.ValueSize = reader.ReadInt32();

            if (obj.ValueSize == 0)
                return true;

            obj.Id = reader.ReadInt32();

            obj.Reserved = reader.ReadByte();

            var remainingBytes = obj.ValueSize - 4 - 4 - 1;
            obj.Value = reader.ReadBytes(remainingBytes);

            return true;
        }

    }
}