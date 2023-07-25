using ShellLink.DataObjects;
using ShellLink.DataObjects.ExtraData;
using ShellLink.Internals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShellLink.Actuators.ExtraData
{
    public sealed class DarwinDataBlockActuator : ExtraDataBlockActuator<DarwinDataBlock>
    {
        public override int MinBlockSize { get; } = 0x00000314;
        public override int MaxBlockSize { get; } = 0x00000314;
        public override int StandardBlockSignature { get; } = unchecked((int)0xA0000006);

        protected override void WriteDataTo(DarwinDataBlock edb, BinaryWriter writer, IOptions options)
        {
            writer.WriteFixedSizeString(edb.DarwinDataAnsi, 260, Encoding.Default);
            writer.WriteFixedSizeString(edb.DarwinDataUnicode, 520, Encoding.Unicode);
        }

        protected override bool LoadData(DarwinDataBlock edb, BinaryReader reader, IOptions options)
        {
            edb.DarwinDataAnsi = reader.ReadFixedSizeString(260, Encoding.Default, ZeroCharBehavior.RemoveTrailing);
            edb.DarwinDataUnicode = reader.ReadFixedSizeString(520, Encoding.Unicode, ZeroCharBehavior.RemoveTrailing);
            return true;
        }

        protected override void CheckData(DarwinDataBlock edb, List<Exception> errors, ShellLinkObject shellLinkObject)
        {
            CheckString(errors, edb.DarwinDataAnsi, 260, nameof(edb.DarwinDataAnsi));
            CheckString(errors, edb.DarwinDataUnicode, 260, nameof(edb.DarwinDataUnicode));
        }

        protected override void RepairData(DarwinDataBlock edb)
        {
            // TODO: this should be the same for:
            // - DarwinDataBlock
            // - EnvironmentVariableDataBlock
            // - IconEnvironmentDataBlock
        }

        public override ExtraDataBlock Read(BinaryReader reader, uint size, int sig, IOptions options)
        {
            throw new NotImplementedException();
        }
    }
}