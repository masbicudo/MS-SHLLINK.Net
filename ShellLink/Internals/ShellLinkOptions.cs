using JetBrains.Annotations;
using ShellLink.ExtraData;

namespace ShellLink.Internals
{
    public sealed class ShellLinkOptions
    {
        public ItemIDProvider ItemIdProvider { get; set; }
        public ExtraDataBlockProvider ExtraDataBlockProvider { get; set; }

        public ShellLinkOptions Clone()
        {
            return new ShellLinkOptions
            {
                ItemIdProvider = this.ItemIdProvider,
                ExtraDataBlockProvider = this.ExtraDataBlockProvider,
            };
        }

        [NotNull]
        public static ShellLinkOptions Normalize([CanBeNull] ShellLinkOptions options)
        {
            bool isNew = false;
            if (options == null)
            {
                options = new ShellLinkOptions();
                isNew = true;
            }

            if (options.ItemIdProvider == null)
            {
                if (!isNew) options = options.Clone();
                options.ItemIdProvider = new ItemIDProvider();
            }

            if (options.ExtraDataBlockProvider == null)
            {
                if (!isNew) options = options.Clone();
                options.ExtraDataBlockProvider = new ExtraDataBlockProvider();
            }

            return options;
        }
    }
}