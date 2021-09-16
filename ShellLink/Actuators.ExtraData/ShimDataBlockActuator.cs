using ShellLink.DataObjects;
using ShellLink.DataObjects.ExtraData;
using ShellLink.Internals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShellLink.Actuators.ExtraData
{
    public sealed class ShimDataBlockActuator : ExtraDataBlockActuator<ShimDataBlock>
    {
        public override int MinBlockSize { get; } = 0x00000088;
        public override int MaxBlockSize { get; } = int.MaxValue;
        public override int StandardBlockSignature { get; } = unchecked((int)0xA0000008);

        protected override void WriteDataTo(ShimDataBlock edb, BinaryWriter writer, IOptions options)
        {
            writer.WriteNullTerminatedString(edb.LayerName, Encoding.Unicode, minSize: 0x80);
        }

        protected override bool LoadData(ShimDataBlock edb, BinaryReader reader, IOptions options)
        {
            edb.LayerName = reader.ReadNullTerminatedString(Encoding.Unicode);
            return true;
        }

        protected override void CheckData(ShimDataBlock edb, List<Exception> errors, ShellLinkObject shellLinkObject)
        {
        }

        protected override void RepairData(ShimDataBlock edb)
        {
            // TODO
        }
    }
}