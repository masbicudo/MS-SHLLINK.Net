using ShellLink.Internals;
using System.Collections.Generic;
using System.IO;

namespace ShellLink.ItemID.Actuators
{
    /// <summary>
    /// ItemIDs must be providaded through a class because they are complex,
    /// and vary a lot from one version of Windows to another. This allows
    /// the caller to choose what types of ItemIDs will be available.
    /// No worries if you don't care about ItemIDs. The default of this
    /// provider is to use the RawItemID, that exposes the raw binary data.
    /// </summary>
    public sealed class ItemIDProvider
    {
        public ItemIDProvider(IOptions options)
        {
            ItemIdReaders = new IItemIDReader[0];
            Options = options;
        }

        public ItemIDProvider(IItemIDReader[] itemIdReaders)
        {
            ItemIdReaders = itemIdReaders;
        }

        public IReadOnlyCollection<IItemIDReader> ItemIdReaders { get; }
        public IOptions Options { get; }

        public ShellItemId Read(BinaryReader reader)
        {
            var size = reader.ReadUInt16();

            if (size == 0)
                return null;

            var buffer = reader.ReadBytes(size - 2);

            foreach (var itemIdReader in ItemIdReaders)
            {
                var subreader = new BinaryReader(new MemoryStream(buffer));
                var itemid = itemIdReader.Read(subreader, Options);

                // must not be null
                // must read everything in the buffer
                if (itemid != null && subreader.BaseStream.Position == buffer.LongLength)
                    return itemid;
            }

            var result = new RawItemID
            {
                ItemIDSize = size,
                Data = buffer,
            };

            return result;
        }
    }
}