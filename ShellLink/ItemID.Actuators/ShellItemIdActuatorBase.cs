using ShellLink.Parameters;
using System;

namespace ShellLink.ItemID.Actuators
{
    public abstract class ShellItemIdActuatorBase<TShellItemId>
        where TShellItemId : ShellItemId
    {
        public virtual bool Check(CheckParams<TShellItemId> arguments)
        {
            var expectedLength = arguments.item.ItemIDSize;
            if (this.GetLength(new(arguments.item, arguments.options)) != expectedLength)
            {
                arguments.errors.Add(new ArgumentException(
                        $"{nameof(arguments.item.ItemIDSize)} is not equal to {nameof(this.GetLength)}()",
                        nameof(arguments.item.ItemIDSize
                    )));
                return false;
            }
            if (!this.CheckData(arguments))
                return false;
            return true;
        }
        protected abstract bool CheckData(CheckParams<TShellItemId> arguments);

        public virtual uint GetLength(LengthParams<TShellItemId> arguments)
        {
            var dataLength = 2u;
            dataLength += this.GetDataLength(new(arguments.item, arguments.options));
            return dataLength;
        }
        protected abstract uint GetDataLength(LengthParams<TShellItemId> arguments);

        public virtual bool Read(ReadParams<TShellItemId> arguments)
        {
            arguments.item.ItemIDSize = arguments.reader.ReadUInt16();
            if (!this.ReadData(arguments))
                return false;
            return true;
        }
        protected abstract bool ReadData(ReadParams<TShellItemId> arguments);

        public virtual bool Repair(RepairParams<TShellItemId> arguments)
        {
            var itemIdSize = this.GetLength(new(arguments.item, arguments.options));
            if (itemIdSize >= 0x10000u)
                throw new NotImplementedException("itemIdSize >= 0x8000u not implemented");
            arguments.item.ItemIDSize = (ushort)itemIdSize;
            if (!this.RepairData(arguments))
                return false;
            return true;
        }
        protected abstract bool RepairData(RepairParams<TShellItemId> arguments);

        public virtual bool Write(WriteParams<TShellItemId> arguments)
        {
            arguments.writer.Write(arguments.item.ItemIDSize);
            if (!this.WriteData(arguments))
                return false;
            return true;
        }
        protected abstract bool WriteData(WriteParams<TShellItemId> arguments);
    }
}