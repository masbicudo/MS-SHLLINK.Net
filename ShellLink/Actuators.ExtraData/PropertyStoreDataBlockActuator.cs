using ShellLink.DataObjects;
using ShellLink.DataObjects.ExtraData;
using ShellLink.Internals;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShellLink.Actuators.ExtraData
{
    public sealed class PropertyStoreDataBlockActuator : ExtraDataBlockActuator<PropertyStoreDataBlock>
    {
        public override int MinBlockSize { get; } = 0x0000000C;
        public override int MaxBlockSize { get; } = int.MaxValue;

        public override int StandardBlockSignature { get; } = unchecked((int)0xA000000B);

        protected override void WriteDataTo(PropertyStoreDataBlock edb, BinaryWriter writer, IOptions options)
        {
            edb.PropertyStore.WriteTo(writer);
        }

        protected override bool LoadData(PropertyStoreDataBlock edb, BinaryReader reader, IOptions options)
        {
            var ok = true;
            ok &= edb.PropertyStore.Load(reader);
            return true;
        }

        protected override void CheckData(PropertyStoreDataBlock edb, List<Exception> errors, ShellLinkObject shellLinkObject)
        {
            // TODO: check whether the IDList contains an ItemID at the given offset
        }

        protected override void RepairData(PropertyStoreDataBlock edb)
        {
            // the error cannot be repaired easily
        }

        public override ExtraDataBlock Read(BinaryReader reader, uint size, int sig, IOptions options)
        {
            throw new NotImplementedException();
        }
    }
}