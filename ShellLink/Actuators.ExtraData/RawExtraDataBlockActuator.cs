using ShellLink.DataObjects;
using ShellLink.DataObjects.ExtraData;
using ShellLink.Internals;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShellLink.Actuators.ExtraData
{
    public class RawExtraDataBlockActuator : ExtraDataBlockActuator<RawExtraDataBlock>
    {
        public override int MinBlockSize { get; } = SizeAndSigFieldLength;
        public override int MaxBlockSize { get; } = int.MaxValue;
        public override int StandardBlockSignature =>
            throw new Exception($"{nameof(RawExtraDataBlockActuator)} does not have a" +
                $" a {nameof(StandardBlockSignature)}.");

        protected override void WriteDataTo(RawExtraDataBlock edb, BinaryWriter writer, IOptions options)
        {
            writer.Write((byte[])edb.Data);
        }

        protected override bool LoadData(RawExtraDataBlock edb, BinaryReader reader, IOptions options)
        {
            if (edb.BlockSize >= 0x80000000u)
                throw new NotImplementedException("Block sizes >= 0x80000000u are not implemented");
            edb.Data = reader.ReadBytes((int)edb.BlockSize);
            return true;
        }

        protected override void CheckData(RawExtraDataBlock edb, List<Exception> errors, ShellLinkObject shellLinkObject)
        {
            if (edb.BlockSize != SizeAndSigFieldLength + edb.Data.Length)
            {
                var name_BlockSize = nameof(edb.BlockSize);
                var name_SizeAndSig = nameof(SizeAndSigFieldLength);
                var name_DataLength = nameof(edb.Data) + "." + nameof(edb.Data.Length);
                errors.Add(new ArgumentException(
                    $"{name_BlockSize} is must be equal to {name_DataLength} + {name_SizeAndSig}\n" +
                    $"[{edb.BlockSize} != {edb.Data.Length} + {SizeAndSigFieldLength}]",
                    name_BlockSize));
            }
        }

        protected override void RepairData(RawExtraDataBlock edb)
        {
            edb.BlockSize = (uint)((uint)SizeAndSigFieldLength + edb.Data.Length);
        }
    }
}