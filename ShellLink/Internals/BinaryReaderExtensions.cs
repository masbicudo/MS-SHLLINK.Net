using System;
using System.IO;
using System.Text;

namespace ShellLink.Internals
{
    public static class BinaryReaderExtensions
    {
        public static Guid ReadGuid(this BinaryReader reader)
        {
            return new Guid(
                reader.ReadInt32(),
                reader.ReadInt16(),
                reader.ReadInt16(),
                reader.ReadByte(),
                reader.ReadByte(),
                reader.ReadByte(),
                reader.ReadByte(),
                reader.ReadByte(),
                reader.ReadByte(),
                reader.ReadByte(),
                reader.ReadByte());
        }

        public static void WriteGuid(this BinaryWriter writer, Guid guid)
        {
            writer.Write(guid.ToByteArray());
        }

        public static DateTime? ReadDateTime(this BinaryReader reader)
        {
            var val = reader.ReadInt64();
            if (val == 0)
                return null;
            return DateTime.FromFileTimeUtc(val);
        }

        public static void WriteDateTime(this BinaryWriter writer, DateTime? dt)
        {
            writer.Write(dt?.ToFileTimeUtc() ?? 0L);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException"></exception>
        public static string ReadNullTerminatedString(this BinaryReader reader, Encoding encoding)
        {
            var result = new StringBuilder();
            var d = encoding.GetDecoder();
            var b = new byte[1];
            var c = new char[1];

            b[0] = reader.ReadByte();
            var nb = 1;
            while (true)
            {
                d.Convert(b, 0, nb, c, 0, 1, false, out int _, out int cu, out bool r);

                if (cu > 0 && c[0] == 0)
                    return result.ToString();
                if (cu > 0) result.Append(c[0]);

                nb = r ? 1 : 0;
                if (r) b[0] = reader.ReadByte();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException"></exception>
        public static void WriteNullTerminatedString(this BinaryWriter writer, string str, Encoding encoding)
        {
            var byteStr = encoding.GetBytes(str + '\0');
            writer.Write(byteStr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="length">The length of the string measured in bytes.</param>
        /// <param name="encoding"></param>
        /// <param name="zcb"></param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException"></exception>
        public static string ReadFixedSizeString(this BinaryReader reader, int length, Encoding encoding, ZeroCharBehavior zcb)
        {
            var b = reader.ReadBytes(length);
            var result = encoding.GetString(b);

            if (zcb == ZeroCharBehavior.RemoveTrailing)
            {
                result = result.TrimEnd('\0');
            }
            else if (zcb == ZeroCharBehavior.SplitFirst)
            {
                var firstZero = result.IndexOf('\0');
                if (firstZero >= 0)
                    result = result.Substring(0, firstZero);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="str"></param>
        /// <param name="length">The length of the string measured in bytes.</param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException"></exception>
        public static void WriteFixedSizeString(this BinaryWriter writer, string str, int length, Encoding encoding)
        {
            var bytes = new byte[length];
            var byteStr = encoding.GetBytes(str);
            Array.Copy(byteStr, bytes, length);
            writer.Write(bytes);
        }
    }

    public enum ZeroCharBehavior
    {
        /// <summary>
        /// Keeps all '\0' characters.
        /// </summary>
        None,

        /// <summary>
        /// Removes only the last '\0' characters.
        /// </summary>
        RemoveTrailing,

        /// <summary>
        /// Removes everything after the first '\0' characters.
        /// </summary>
        SplitFirst,
    }
}