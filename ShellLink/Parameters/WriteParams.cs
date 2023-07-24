using ShellLink.Internals;
using System.IO;

namespace ShellLink.Parameters
{
    public struct WriteParams<TItem>
    {
        public TItem item;
        public BinaryWriter writer;
        public IOptions options;

        public WriteParams<TOtherItem> ConvertTo<TOtherItem>()
        {
            return new()
            {
                item = (TOtherItem)(object)this.item,
                writer = this.writer,
                options = this.options,
            };
        }
    }
}