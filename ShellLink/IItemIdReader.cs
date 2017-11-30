using System.IO;

namespace ShellLink
{
    public interface IItemIdReader
    {
        ItemId Read(BinaryReader reader);
    }
}