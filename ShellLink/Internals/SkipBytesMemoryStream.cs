using System.IO;

namespace ShellLink.Internals
{
    internal class SkipBytesMemoryStream : MemoryStream
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