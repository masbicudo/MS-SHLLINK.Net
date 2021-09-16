using ShellLink.DataObjects;
using ShellLink.DataObjects.ExtraData;
using ShellLink.Internals;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShellLink.Actuators.ExtraData
{
    public sealed class NullExtraDataBlockActuator : ExtraDataBlockActuator<NullExtraDataBlock>
    {
        public static NullExtraDataBlockActuator Default
            = new NullExtraDataBlockActuator();
        public override int MinBlockSize => 0;
        public override int MaxBlockSize => 0;
        public override int StandardBlockSignature => throw NullExtraDataBlock.BlockSignatureError();

        protected override void WriteDataTo(NullExtraDataBlock edb, BinaryWriter writer, IOptions options)
        {
            throw NullExtraDataBlock.BlockDataError();
        }

        protected override bool LoadData(NullExtraDataBlock edb, BinaryReader reader, IOptions options)
        {
            throw NullExtraDataBlock.BlockDataError();
        }

        protected override void CheckData(NullExtraDataBlock edb, List<Exception> errors, ShellLinkObject shellLinkObject)
        {
            throw NullExtraDataBlock.BlockDataError();
        }

        protected override void RepairData(NullExtraDataBlock edb)
        {
            throw NullExtraDataBlock.BlockDataError();
        }
    }
}