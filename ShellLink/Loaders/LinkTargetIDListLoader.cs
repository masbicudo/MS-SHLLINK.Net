using System.IO;
using JetBrains.Annotations;
using ShellLink.DataObjects;
using ShellLink.Internals;

namespace ShellLink.Loaders
{
    public static class LinkTargetIDListLoader
    {
        public static bool Load(
                [NotNull] this LinkTargetIDList obj,
                BinaryReader reader,
                IOptions options
            )
        {
            var provider = options.Get<ItemIDProvider>();

            obj.IDListSize = reader.ReadUInt16();

            while (true)
            {
                var itemid = provider.Read(reader);

                if (itemid == null)
                    break;

                obj.IDList.ItemIDList.Add(itemid);
            }

            return true;
        }
    }
}