using System.IO;

namespace ShellLink
{
    public static class ItemIDExtensions
    {
        public static byte[] GetData(this ItemId itemid)
        {
            var sz = itemid.GetDataLength();
            var buffer = new byte[sz];
            itemid.WriteTo(new BinaryWriter(new SkipBytesMemoryStream(buffer, 2)));
            return buffer;
        }

        class SkipBytesMemoryStream : MemoryStream
        {
            private int eatBytes;

            public SkipBytesMemoryStream(byte[] buffer, int eatBytes)
                : base(buffer, true)
            {
                this.eatBytes = eatBytes;
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                var eat = count - this.eatBytes;
                eat = eat < 0 ? 0 : eat;
                this.eatBytes -= eat;
                if (this.eatBytes == 0)
                    base.Write(buffer, offset + eat, count - eat);
            }

            public override void WriteByte(byte value)
            {
                if (this.eatBytes == 0)
                    base.WriteByte(value);
                else
                    this.eatBytes--;
            }
        }
    }
}