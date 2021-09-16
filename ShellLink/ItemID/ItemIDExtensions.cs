using ShellLink.Internals;
using System.IO;

namespace ShellLink
{
    public static class ItemIDExtensions
    {
        public static byte[] GetData(this ItemID itemid, IOptions options)
        {
            var sz = itemid.GetLength() - ItemID.SizeFieldLength;
            var buffer = new byte[sz];
            itemid.WriteTo(
                    new BinaryWriter(new SkipBytesMemoryStream(buffer, ItemID.SizeFieldLength)),
                    options
                );
            return buffer;
        }
    }
}