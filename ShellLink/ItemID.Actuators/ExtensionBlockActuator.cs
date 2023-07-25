using ShellLink.Parameters;

namespace ShellLink.ItemID.Actuators
{
    public abstract class ExtensionBlockActuator :
        IActuator<ExtensionBlock>
    {
        #region Check
        public bool Check(CheckParams<ExtensionBlock> arguments)
        {
            return true;
        }
        #endregion

        #region Length
        public uint GetLength(LengthParams<ExtensionBlock> arguments)
        {
            return 64u;
        }
        #endregion

        #region Read
        public virtual bool Read(ReadParams<ExtensionBlock> arguments)
        {
            arguments.item.Size = arguments.reader.ReadUInt16();
            arguments.item.Version = arguments.reader.ReadUInt16();
            arguments.item.Signature = arguments.reader.ReadUInt32();
            return true;
        }
        #endregion

        #region Repair
        public bool Repair(RepairParams<ExtensionBlock> arguments)
        {
            return true;
        }
        #endregion
        #region Write
        public virtual bool Write(WriteParams<ExtensionBlock> arguments)
        {
            arguments.writer.Write(arguments.item.Size);
            arguments.writer.Write(arguments.item.Version);
            arguments.writer.Write(arguments.item.Signature);
            return true;
        }
        #endregion
    }
}