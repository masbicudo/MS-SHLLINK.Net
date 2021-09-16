using System;
using System.Collections.Generic;
using System.IO;
using ShellLink.DataObjects;
using ShellLink.Internals;

namespace ShellLink.ExtraData
{
    public sealed class EmptyExtraDataBlock : ExtraDataBlock
    {
        public override int BlockSize { get; set; }

        public override int BlockSignature
        {
            get => throw new InvalidOperationException();
            set => throw new InvalidOperationException();
        }

        protected override int GetDataLength()
        {
            throw new InvalidOperationException();
        }

        public override int GetSignatureValue()
        {
            throw new InvalidOperationException();
        }

        protected override void WriteDataTo(BinaryWriter writer, IOptions options)
        {
            throw new InvalidOperationException();
        }

        protected override bool LoadData(BinaryReader reader, IOptions options)
        {
            throw new InvalidOperationException();
        }

        protected override void CheckData(List<Exception> errors, ShellLinkObject shellLinkObject)
        {
            throw new InvalidOperationException();
        }

        protected override void RepairData()
        {
            throw new InvalidOperationException();
        }
    }
}