using System.IO;

namespace ShellLink.ExtraData
{
    public interface IExtraDataBlockReader
    {
        ExtraDataBlock Read(BinaryReader reader, int size, int sig);
    }
}