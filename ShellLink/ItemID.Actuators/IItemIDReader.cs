using ShellLink.Internals;
using System.IO;

namespace ShellLink.ItemID.Actuators
{
    public interface IItemIDReader
    {
        ShellItemId Read(BinaryReader reader, IOptions options);
    }
    public interface IItemIDReader<T> :
        IItemIDReader
        where T : ShellItemId, new()
    {
        T Read(BinaryReader reader, IOptions options);
    }
}