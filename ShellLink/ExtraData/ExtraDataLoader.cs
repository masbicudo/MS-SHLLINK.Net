using System.IO;
using JetBrains.Annotations;
using ShellLink.Internals;

namespace ShellLink.ExtraData
{
    public static class ExtraDataLoader
    {
        public static bool Load(
                [NotNull] this ExtraData obj,
                BinaryReader reader,
                IOptions options
            )
        {
            var provider = options.Get<ExtraDataBlockProvider>();
            while (true)
            {
                var block = provider.Read(reader);

                if (block is EmptyExtraDataBlock)
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