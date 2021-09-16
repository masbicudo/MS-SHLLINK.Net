using ShellLink.DataObjects;
using ShellLink.DataObjects.ExtraData;
using ShellLink.Internals;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShellLink.Actuators.ExtraData
{
    public sealed class KnownFolderDataBlockActuator : ExtraDataBlockActuator<KnownFolderDataBlock>
    {
        public override int MinBlockSize { get; } = 0x0000001C;
        public override int MaxBlockSize { get; } = 0x0000001C;
        public override int StandardBlockSignature { get; } = unchecked((int)0xA000000B);

        protected override void WriteDataTo(KnownFolderDataBlock edb, BinaryWriter writer, IOptions options)
        {
            writer.WriteGuid(edb.KnownFolderID);
            writer.Write((int)edb.Offset);
        }

        protected override bool LoadData(KnownFolderDataBlock edb, BinaryReader reader, IOptions options)
        {
            edb.KnownFolderID = reader.ReadGuid();
            edb.Offset = reader.ReadInt32();
            return true;
        }

        protected override void CheckData(KnownFolderDataBlock edb, List<Exception> errors, ShellLinkObject shellLinkObject)
        {
            // TODO: check whether the IDList contains an ItemID at the given offset
        }

        protected override void RepairData(KnownFolderDataBlock edb)
        {
            // the error cannot be repaired easily
        }
    }
}