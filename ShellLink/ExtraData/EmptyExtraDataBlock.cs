using System;
using System.Collections.Generic;
using System.IO;

namespace ShellLink.ExtraData
{
    public sealed class EmptyExtraDataBlock : ExtraDataBlock
    {
        public override int BlockSize { get; set; }

        public override int BlockSignature
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        protected override int GetDataLength()
        {
            throw new NotImplementedException();
        }

        public override int GetSignatureValue()
        {
            throw new NotImplementedException();
        }

        protected override void WriteDataTo(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        protected override bool LoadData(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        protected override void CheckData(List<Exception> errors)
        {
            throw new NotImplementedException();
        }

        protected override void RepairData()
        {
            throw new NotImplementedException();
        }
    }
}