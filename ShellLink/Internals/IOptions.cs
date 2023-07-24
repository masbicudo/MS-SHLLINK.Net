using ShellLink.ItemID;
using ShellLink.ItemID.Actuators;
using System;

namespace ShellLink.Internals
{
    public interface IOptions
    {
        T Get<T>();
        IActuator<ShellItemId> GetActuatorFor(Type type);
    }
}