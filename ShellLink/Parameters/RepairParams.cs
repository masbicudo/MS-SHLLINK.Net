using ShellLink.Internals;

namespace ShellLink.Parameters
{
    public struct RepairParams<TItem>
    {
        public TItem item;
        public IOptions options;

        public RepairParams(TItem item, IOptions options)
        {
            this.item = item;
            this.options = options;
        }

        public RepairParams<TOtherItem> ConvertTo<TOtherItem>()
        {
            return new()
            {
                item = (TOtherItem)(object)this.item,
            };
        }
    }
}