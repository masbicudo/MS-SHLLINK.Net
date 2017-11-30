using System;

namespace ShellLink.Internals
{
    public static class BinaryHelper
    {
        public static Int32 ReadInt32FromBuffer(byte[] buffer, int position)
        {
            if (buffer.Length - position < sizeof(Int32))
                throw new ArgumentOutOfRangeException(nameof(position), $"Position must be an index that has at least {sizeof(Int32)} bytes available.");

            uint u0 = buffer[position + 0];
            uint u1 = buffer[position + 1];
            uint u2 = buffer[position + 2];
            uint u3 = buffer[position + 3];

            uint r = (u3 << 24) + (u2 << 16) + (u1 << 8) + u0;

            return (Int32)r;
        }

        public static void WriteInt32ToBuffer(byte[] buffer, int position, Int32 value)
        {
            if (buffer.Length - position < sizeof(Int32))
                throw new ArgumentOutOfRangeException(nameof(position), $"Position must be an index that has at least {sizeof(Int32)} bytes available.");

            buffer[position + 0] = (byte)value;
            buffer[position + 1] = (byte)(value >> 8);
            buffer[position + 2] = (byte)(value >> 16);
            buffer[position + 3] = (byte)(value >> 24);
        }

        public static Int64 ReadInt64FromBuffer(byte[] buffer, int position)
        {
            if (buffer.Length - position < sizeof(Int64))
                throw new ArgumentOutOfRangeException(nameof(position), $"Position must be an index that has at least {sizeof(Int64)} bytes available.");

            ulong u0 = buffer[position + 0];
            ulong u1 = buffer[position + 1];
            ulong u2 = buffer[position + 2];
            ulong u3 = buffer[position + 3];
            ulong u4 = buffer[position + 4];
            ulong u5 = buffer[position + 5];
            ulong u6 = buffer[position + 6];
            ulong u7 = buffer[position + 7];

            ulong r = (u7 << 56) + (u6 << 48) + (u5 << 40) + (u4 << 32) + (u3 << 24) + (u2 << 16) + (u1 << 8) + u0;

            return (Int64)r;
        }

        public static void WriteInt64ToBuffer(byte[] buffer, int position, Int64 value)
        {
            if (buffer.Length - position < sizeof(Int64))
                throw new ArgumentOutOfRangeException(nameof(position), $"Position must be an index that has at least {sizeof(Int64)} bytes available.");

            buffer[position + 0] = (byte)value;
            buffer[position + 1] = (byte)(value >> 8);
            buffer[position + 2] = (byte)(value >> 16);
            buffer[position + 3] = (byte)(value >> 24);
            buffer[position + 4] = (byte)(value >> 32);
            buffer[position + 5] = (byte)(value >> 40);
            buffer[position + 6] = (byte)(value >> 48);
            buffer[position + 7] = (byte)(value >> 56);
        }

        public static Int16 ReadInt16FromBuffer(byte[] buffer, int position)
        {
            if (buffer.Length - position < sizeof(Int16))
                throw new ArgumentOutOfRangeException(nameof(position), $"Position must be an index that has at least {sizeof(Int16)} bytes available.");

            uint u0 = buffer[position + 0];
            uint u1 = buffer[position + 1];

            uint r = (u1 << 8) + u0;

            return (Int16)r;
        }

        public static void WriteInt16ToBuffer(byte[] buffer, int position, Int16 value)
        {
            if (buffer.Length - position < sizeof(Int16))
                throw new ArgumentOutOfRangeException(nameof(position), $"Position must be an index that has at least {sizeof(Int16)} bytes available.");

            buffer[position + 0] = (byte)value;
            buffer[position + 1] = (byte)(value >> 8);
        }

        public static Guid ReadGuidFromBuffer(byte[] buffer, int position)
        {
            if (buffer.Length - position < 16)
                throw new ArgumentOutOfRangeException(nameof(position), $"Position must be an index that has at least {16} bytes available.");

            var a = ReadInt32FromBuffer(buffer, position + 0);
            var b = ReadInt16FromBuffer(buffer, position + 4);
            var c = ReadInt16FromBuffer(buffer, position + 6);
            var d = buffer[position + 8];
            var e = buffer[position + 9];
            var f = buffer[position + 10];
            var g = buffer[position + 11];
            var h = buffer[position + 12];
            var i = buffer[position + 13];
            var j = buffer[position + 14];
            var k = buffer[position + 15];

            return new Guid(a, b, c, d, e, f, g, h, i, j, k);
        }

        public static void WriteGuidToBuffer(byte[] buffer, int position, Guid value)
        {
            if (buffer.Length - position < 16)
                throw new ArgumentOutOfRangeException(nameof(position), $"Position must be an index that has at least {16} bytes available.");

            var b = value.ToByteArray();
            for (int it = 0; it < b.Length; it++)
                buffer[position + it] = b[it];
        }

        public static DateTime? ReadDateTimeFromBuffer(byte[] buffer, int position)
        {
            var data = ReadInt64FromBuffer(buffer, position);

            if (data == 0)
                return null;

            var dt = DateTime.FromFileTimeUtc(data);
            return dt;
        }

        public static void WriteDateTimeToBuffer(byte[] buffer, int position, DateTime? value)
        {
            if (value == null)
            {
                WriteInt64ToBuffer(buffer, position, value.Value.ToFileTimeUtc());
            }
        }
    }
}