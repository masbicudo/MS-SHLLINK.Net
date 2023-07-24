using ShellLink.Internals;
using ShellLink.ItemID.Actuators;
using ShellLink.ItemID;
using System.IO;

namespace ShellLink.ItemID.Actuators
{
    public abstract class TypeIndicatorBasedItemIDActuatorBase<TShellItemId> : ShellItemIdActuatorBase<TShellItemId>
        where TShellItemId : TypeIndicatorBasedItemID
    {
    }
}
