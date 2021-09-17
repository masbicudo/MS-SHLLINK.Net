using JetBrains.Annotations;
using ShellLink.DataObjects.ExtraData;
using ShellLink.Internals;
using System.IO;

namespace ShellLink.Actuators
{
    public static class ExtraDataLoader
    {
        public static bool Load(
                [NotNull] this ShellLink.DataObjects.ExtraData.ExtraData obj,
                BinaryReader reader,
                IOptions options
            )
        {
            var provider = options.Get<ExtraDataBlockProvider>();
            while (true)
            {
                var block = provider.Read(reader);

                if (block is NullExtraDataBlock)
                {
                    obj.TerminalID = block.BlockSize;
                    break;
                }

                obj.ExtraDataBlock.Add(block);
            }

            return true;
        }
    }
}