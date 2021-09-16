using ShellLink.DataObjects;
using ShellLink.DataObjects.ExtraData;
using ShellLink.Internals;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShellLink.Actuators.ExtraData
{
    public class ConsoleFEDataBlockActuator : ExtraDataBlockActuator<ConsoleFEDataBlock>
    {
        public override int MinBlockSize { get; } = 0x0000000C;
        public override int MaxBlockSize { get; } = 0x0000000C;

        public override int StandardBlockSignature => unchecked((int)0xA0000004);

        protected override void WriteDataTo(ConsoleFEDataBlock edb, BinaryWriter writer, IOptions options)
        {
            writer.Write((int)edb.CodePage);
        }

        protected override bool LoadData(ConsoleFEDataBlock edb, BinaryReader reader, IOptions options)
        {
            edb.CodePage = reader.ReadInt32();
            return true;
        }

        protected override void CheckData(ConsoleFEDataBlock edb, List<Exception> errors, ShellLinkObject shellLinkObject)
        {
        }

        protected override void RepairData(ConsoleFEDataBlock edb)
        {
            // TODO
        }
    }
}