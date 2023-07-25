using ShellLink.DataObjects;
using ShellLink.DataObjects.ExtraData;
using ShellLink.Internals;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShellLink.Actuators.ExtraData
{
    public abstract class ExtraDataBlockActuator<T> : ExtraDataBlockActuator
        where T : ExtraDataBlock, new()
    {
        public void WriteTo(T extraDataBlock, BinaryWriter writer, IOptions options)
        {
            writer.Write((uint)extraDataBlock.BlockSize);

            if (extraDataBlock is NullExtraDataBlock)
                return;

            writer.Write((uint)extraDataBlock.BlockSignature);

            this.WriteDataTo(extraDataBlock, writer, options);
        }

        protected abstract void WriteDataTo(T extraDataBlock, BinaryWriter writer, IOptions options);

        public bool Load(T extraDataBlock, BinaryReader reader, bool skipHeader = false, IOptions options = null)
        {
            if (!skipHeader)
            {
                extraDataBlock.BlockSize = reader.ReadUInt16();

                if (this is NullExtraDataBlock)
                    return true;

                extraDataBlock.BlockSignature = reader.ReadInt32();
            }

            bool ok = this.LoadData(extraDataBlock, reader, options);
            return ok;
        }

        protected abstract bool LoadData(T extraDataBlock, BinaryReader reader, IOptions options);

        public void Check(T extraDataBlock, List<Exception> errors, ShellLinkObject shellLinkObject)
        {
            if (extraDataBlock.BlockSize < this.MinBlockSize)
            {
                var name_BlockSize = nameof(extraDataBlock.BlockSize);
                var name_MinBlockSize = nameof(this.MinBlockSize);
                errors.Add(new ArgumentException(
                    $"{name_BlockSize} is lesser than {name_MinBlockSize}\n" +
                    $"[{extraDataBlock.BlockSize} < {this.MinBlockSize}]",
                    name_BlockSize));
            }

            if (extraDataBlock.BlockSize > this.MaxBlockSize)
            {
                var name_BlockSize = nameof(extraDataBlock.BlockSize);
                var name_MaxBlockSize = nameof(this.MaxBlockSize);
                errors.Add(new ArgumentException(
                    $"{name_BlockSize} is greater than {name_MaxBlockSize}\n" +
                    $"[{extraDataBlock.BlockSize} > {this.MaxBlockSize}]",
                    name_BlockSize));
            }

            if (this is NullExtraDataBlock)
                return;

            if (!(this is RawExtraDataBlock))
                if (extraDataBlock.BlockSignature != this.StandardBlockSignature)
                {
                    var name_BlockSignature = nameof(extraDataBlock.BlockSignature);
                    var name_Signature = nameof(this.StandardBlockSignature);
                    errors.Add(new ArgumentException(
                        $"{name_BlockSignature} is not equal to {name_Signature}\n" +
                        $"[{extraDataBlock.BlockSignature} != {this.StandardBlockSignature}]",
                        name_BlockSignature));
                }

            this.CheckData(extraDataBlock, errors, shellLinkObject);
        }

        protected abstract void CheckData(T extraDataBlock, List<Exception> errors, ShellLinkObject shellLinkObject);

        public void Repair(T extraDataBlock)
        {
            if (extraDataBlock.BlockSize < this.MinBlockSize)
                extraDataBlock.BlockSize = (ushort)(this.MinBlockSize);
            if (extraDataBlock.BlockSize > this.MaxBlockSize)
                extraDataBlock.BlockSize = (ushort)(this.MaxBlockSize);
            extraDataBlock.BlockSignature = this.StandardBlockSignature;
            this.RepairData(extraDataBlock);
        }

        protected abstract void RepairData(T extraDataBlock);
        public override ExtraDataBlock Read(BinaryReader reader, uint size, int sig, IOptions options)
        {
            if (sig != this.StandardBlockSignature)
                return null;

            var block = new T()
            {
                BlockSize = size,
                BlockSignature = sig
            };

            this.Load(block, reader, skipHeader: true, options);
            return block;
        }
    }
}