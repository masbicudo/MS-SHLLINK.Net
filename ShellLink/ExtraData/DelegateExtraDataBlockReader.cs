using ShellLink.Internals;
using System;
using System.IO;

namespace ShellLink.ExtraData
{
    public sealed class DelegateExtraDataBlockReader :
        IExtraDataBlockReader
    {
        private readonly Func<BinaryReader, ExtraDataBlock> readDelegate;

        public DelegateExtraDataBlockReader(Func<BinaryReader, ExtraDataBlock> readDelegate)
        {
            this.readDelegate = readDelegate;
        }

        public ExtraDataBlock Read(BinaryReader reader, int size, int sig, IOptions options)
        {
            var result = this.readDelegate?.Invoke(reader);
            return result;
        }
    }
}