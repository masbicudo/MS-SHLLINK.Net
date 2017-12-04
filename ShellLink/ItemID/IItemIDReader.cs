using System.IO;

namespace ShellLink
{
    public interface IItemIDReader
    {
        ItemID Read(BinaryReader reader);
    }
}