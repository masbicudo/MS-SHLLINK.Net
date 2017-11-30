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

        public static DateTime? ReadDateTime(this BinaryReader reader)
        {
            var val = reader.ReadInt64();
            if (val == 0)
                return null;
            return DateTime.FromFileTimeUtc(val);
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
        /// <param name="reader"></param>
        /// <param name="length">The length of the string measured in bytes.</param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException"></exception>
        public static string ReadFixedSizeString(this BinaryReader reader, int length, Encoding encoding)
        {
            var b = reader.ReadBytes(length);
            var result = encoding.GetString(b);
            return result;
        }
    }
}