using ShellLink.Internals;
using System;
using System.IO;

namespace ShellLink.Parameters
{
    public struct ReadParams<TItem>
    {
        public TItem item;
        public BinaryReader reader;
        public IOptions options;

        public ReadParams(TItem item, BinaryReader reader, IOptions options)
        {
            this.item = item;
            this.reader = reader;
            this.options = options;
        }

        public ReadParams<TOtherItem> ConvertTo<TOtherItem>()
        {
            return new()
            {
                item = (TOtherItem)(object)this.item,
                reader = this.reader,
                options = this.options,
            };
        }
    }
}