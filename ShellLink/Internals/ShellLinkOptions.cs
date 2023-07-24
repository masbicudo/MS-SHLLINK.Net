using JetBrains.Annotations;
using ShellLink.Actuators;
using ShellLink.ItemID;
using ShellLink.ItemID.Actuators;
using System;
using System.IO;

namespace ShellLink.Internals
{
    public sealed class ShellLinkOptions :
        IOptions
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
                options.ItemIdProvider = new ItemIDProvider(options);
            }

            if (options.ExtraDataBlockProvider == null)
            {
                if (!isNew) options = options.Clone();
                options.ExtraDataBlockProvider = new ExtraDataBlockProvider(options);
            }

            return options;
        }

        T IOptions.Get<T>()
        {
            if (typeof(T) == typeof(ShellLinkOptions))
                return (T)(object)this;
            if (typeof(T) == typeof(ItemIDProvider))
                return (T)(object)this.ItemIdProvider;
            if (typeof(T) == typeof(ExtraDataBlockProvider))
                return (T)(object)this.ExtraDataBlockProvider;
            return default(T);
        }

        public static ShellLinkOptions AllLoaders()
        {
            return new ShellLinkOptions
            {
                ItemIdProvider = new ItemIDProvider(new IItemIDReader[]
                {
                    //new RootFolderShellItemActuator(),
                })
            };
        }

        public IActuator<ShellItemId> GetActuatorFor(Type type)
        {
            throw new NotImplementedException();
        }
    }
}