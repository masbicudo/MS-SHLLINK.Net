using ShellLink.DataObjects;
using ShellLink.DataObjects.ExtraData;
using ShellLink.Internals;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShellLink.Actuators.ExtraData
{
    public sealed class SpecialFolderDataBlockActuator : ExtraDataBlockActuator<SpecialFolderDataBlock>
    {
        public override int MinBlockSize { get; } = 0x00000010;
        public override int MaxBlockSize { get; } = 0x00000010;
        public override int StandardBlockSignature { get; } = unchecked((int)0xA0000005);

        protected override void WriteDataTo(SpecialFolderDataBlock edb, BinaryWriter writer, IOptions options)
        {
            writer.Write((int)edb.SpecialFolderID);
            writer.Write((int)edb.Offset);
        }

        protected override bool LoadData(SpecialFolderDataBlock edb, BinaryReader reader, IOptions options)
        {
            edb.SpecialFolderID = reader.ReadInt32();
            edb.Offset = reader.ReadInt32();
            return true;
        }

        protected override void CheckData(SpecialFolderDataBlock edb, List<Exception> errors, ShellLinkObject shellLinkObject)
        {
        }

        protected override void RepairData(SpecialFolderDataBlock edb)
        {
            // TODO
        }

        public override ExtraDataBlock Read(BinaryReader reader, uint size, int sig, IOptions options)
        {
            throw new NotImplementedException();
        }
    }
}