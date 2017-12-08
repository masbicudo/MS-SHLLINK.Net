using System;
using System.Collections.Generic;
using System.IO;
using ShellLink.DataObjects;

namespace ShellLink.ExtraData
{
    public class RawExtraDataBlock : ExtraDataBlock
    {
        public byte[] Data { get; set; }

        public override int BlockSize { get; set; }

        public override int BlockSignature { get; set; }

        protected override int GetDataLength() => this.Data.Length;

        public override int GetSignatureValue() => this.BlockSignature;

        protected override void WriteDataTo(BinaryWriter writer)
        {
            writer.Write((byte[]) this.Data);
        }

        protected override bool LoadData(BinaryReader reader)
        {
            this.BlockSize = reader.ReadInt32();
            this.BlockSignature = reader.ReadInt32();
            this.Data = reader.ReadBytes(this.BlockSize);
            return true;
        }

        protected override void CheckData(List<Exception> errors, ShellLinkObject shellLinkObject)
        {
        }

        protected override void RepairData()
        {
            // TODO
        }
    }
}