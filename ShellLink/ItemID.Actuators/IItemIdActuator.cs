using ShellLink.Parameters;

namespace ShellLink.ItemID.Actuators
{
    public interface IItemIdActuator<TItem> :
        IActuator<TItem>
    {
        bool ReadData(ReadParams<TItem> arguments);
        bool WriteData(WriteParams<TItem> arguments);
        int GetDataLength(LengthParams<TItem> arguments);
        bool CheckData(CheckParams<TItem> arguments);
        bool RepairData(RepairParams<TItem> arguments);
    }
}