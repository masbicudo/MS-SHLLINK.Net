using ShellLink.Parameters;
using System;

namespace ShellLink.ItemID.Actuators
{
    public sealed class RawShellItemIDActuator : ShellItemIdActuatorBase<RawItemID>,
        IActuator<RawItemID>
    {
        protected override bool CheckData(CheckParams<RawItemID> arguments)
        {
            return true;
        }

        protected override uint GetDataLength(LengthParams<RawItemID> arguments)
        {
            return (uint)arguments.item.Data.Length;
        }

        protected override bool ReadData(ReadParams<RawItemID> arguments)
        {
            arguments.item.Data = arguments.reader.ReadBytes(arguments.item.ItemIDSize);
            return true;
        }

        protected override bool RepairData(RepairParams<RawItemID> arguments)
        {
            return true;
        }

        protected override bool WriteData(WriteParams<RawItemID> arguments)
        {
            if (arguments.item.Data != null)
                arguments.writer.Write(arguments.item.Data);
            return true;
        }
    }
}