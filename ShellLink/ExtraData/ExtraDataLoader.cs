using System.IO;
using JetBrains.Annotations;

namespace ShellLink.ExtraData
{
    public static class ExtraDataLoader
    {
        public static bool Load([NotNull] this ExtraData obj, BinaryReader reader, ExtraDataBlockProvider provider)
        {
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