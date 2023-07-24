using ShellLink.Internals;

namespace ShellLink.Parameters
{
    public struct LengthParams<TItem>
    {
        public TItem item;
        public IOptions options;

        public LengthParams(TItem item, IOptions options) : this()
        {
            this.item = item;
            this.options = options;
        }

        public LengthParams<TOtherItem> ConvertTo<TOtherItem>()
        {
            return new()
            {
                item = (TOtherItem)(object)this.item,
                options = this.options,
            };
        }
    }
}