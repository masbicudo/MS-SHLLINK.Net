using ShellLink.Parameters;

namespace ShellLink.ItemID.Actuators
{
    public interface IActuator<TItem>
    {
        bool Read(ReadParams<TItem> arguments);
        bool Write(WriteParams<TItem> arguments);
        int GetLength(LengthParams<TItem> arguments);
        bool Check(CheckParams<TItem> arguments);
        bool Repair(RepairParams<TItem> arguments);
    }
}