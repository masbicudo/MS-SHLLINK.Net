using System.IO;

namespace ShellLink.ExtraData
{
    public sealed class ExtraDataBlockReader<TDataBlock> :
        IExtraDataBlockReader
        where TDataBlock : ExtraDataBlock, new()
    {
        public ExtraDataBlock Read(BinaryReader reader, int size, int sig)
        {
            TDataBlock block = new TDataBlock();

            if (sig != block.GetSignatureValue())
                return null;

            block.BlockSize = size;
            block.BlockSignature = sig;

            block.Load(reader, skipHeader: true);
            return block;
        }
    }
}