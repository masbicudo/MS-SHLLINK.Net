using System;
using System.Collections.Generic;
using System.IO;
using ShellLink.DataObjects;

namespace ShellLink.ExtraData
{
    public abstract class ExtraDataBlock
    {
        /// <summary>
        /// A 16-bit, unsigned integer that specifies the size, in bytes,
        /// of the ItemID structure, including the ItemIDSize field.
        /// </summary>
        public abstract int BlockSize { get; set; }

        public abstract int BlockSignature { get; set; }

        public const int SizeAndSigFieldLength = sizeof(uint) * 2;

        public int GetLength()
        {
            return this.GetDataLength() + SizeAndSigFieldLength;
        }

        protected abstract int GetDataLength();

        public abstract int GetSignatureValue();

        public void WriteTo(BinaryWriter writer)
        {
            writer.Write((uint)this.BlockSize);

            if (this is EmptyExtraDataBlock)
                return;

            writer.Write((uint)this.BlockSignature);

            this.WriteDataTo(writer);
        }

        protected abstract void WriteDataTo(BinaryWriter writer);

        public bool Load(BinaryReader reader, bool skipHeader = false)
        {
            if (!skipHeader)
            {
                this.BlockSize = reader.ReadInt32();

                if (this is EmptyExtraDataBlock)
                    return true;

                this.BlockSignature = reader.ReadInt32();
            }

            return this.LoadData(reader);
        }

        protected abstract bool LoadData(BinaryReader reader);

        public void Check(List<Exception> errors, ShellLinkObject shellLinkObject)
        {
            if (this.BlockSize != this.GetDataLength() + SizeAndSigFieldLength)
                errors.Add(new ArgumentException(
                    $"{nameof(BlockSize)} is not equal to {nameof(GetDataLength)}() + {SizeAndSigFieldLength}", nameof(this.BlockSize)));

            if (this is EmptyExtraDataBlock)
                return;

            if (this.BlockSignature != this.GetSignatureValue())
                errors.Add(new ArgumentException(
                    $"{nameof(BlockSignature)} is not equal to {nameof(GetSignatureValue)}()", nameof(this.BlockSignature)));

            this.CheckData(errors, shellLinkObject);
        }

        protected abstract void CheckData(List<Exception> errors, ShellLinkObject shellLinkObject);

        public void Repair()
        {
            this.BlockSize = (ushort)(this.GetDataLength() + SizeAndSigFieldLength);
            this.BlockSignature = this.GetSignatureValue();
        }

        protected abstract void RepairData();

        protected static void CheckString(List<Exception> errors, string str, int maxsize, string fieldName)
        {
            if (str.Length >= maxsize)
                errors.Add(new ArgumentException($"Maximum size of {fieldName} is {maxsize - 1} chars.", fieldName));
            if (str.IndexOf('\0') >= 0)
                errors.Add(new ArgumentException($"{fieldName} must not contain '\\0' characters.", fieldName));
        }
    }
}