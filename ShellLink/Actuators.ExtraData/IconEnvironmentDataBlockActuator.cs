using ShellLink.DataObjects;
using ShellLink.DataObjects.ExtraData;
using ShellLink.Internals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShellLink.Actuators.ExtraData
{
    public sealed class IconEnvironmentDataBlockActuator : ExtraDataBlockActuator<IconEnvironmentDataBlock>
    {
        public override int MinBlockSize { get; } = 0x00000314;
        public override int MaxBlockSize { get; } = 0x00000314;
        public override int StandardBlockSignature { get; } = unchecked((int)0xA0000007);

        protected override void WriteDataTo(IconEnvironmentDataBlock edb, BinaryWriter writer, IOptions options)
        {
            writer.WriteFixedSizeString(edb.TargetAnsi, 260, Encoding.Default);
            writer.WriteFixedSizeString(edb.TargetUnicode, 520, Encoding.Unicode);
        }

        protected override bool LoadData(IconEnvironmentDataBlock edb, BinaryReader reader, IOptions options)
        {
            edb.TargetAnsi = reader.ReadFixedSizeString(260, Encoding.Default, ZeroCharBehavior.RemoveTrailing);
            edb.TargetUnicode = reader.ReadFixedSizeString(520, Encoding.Unicode, ZeroCharBehavior.RemoveTrailing);
            return true;
        }

        protected override void CheckData(IconEnvironmentDataBlock edb, List<Exception> errors, ShellLinkObject shellLinkObject)
        {
            CheckString(errors, edb.TargetAnsi, 260, nameof(edb.TargetAnsi));
            CheckString(errors, edb.TargetUnicode, 260, nameof(edb.TargetUnicode));
        }

        protected override void RepairData(IconEnvironmentDataBlock edb)
        {
            // TODO: this should be the same for:
            // - DarwinDataBlock
            // - EnvironmentVariableDataBlock
            // - IconEnvironmentDataBlock
        }
    }
}