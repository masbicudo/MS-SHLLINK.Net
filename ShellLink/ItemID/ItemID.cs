using ShellLink.Internals;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShellLink
{
    /// <summary>
    /// An ItemID is an element in an IDList structure (section 2.2.1).
    /// The data stored in a given ItemID is defined by the source that
    /// corresponds to the location in the target namespace of the
    /// preceding ItemIDs. This data uniquely identifies the items in
    /// that part of the namespace.
    /// </summary>
    public abstract class ItemID
    {
        /// <summary>
        /// A 16-bit, unsigned integer that specifies the size, in bytes,
        /// of the ItemID structure, including the ItemIDSize field.
        /// </summary>
        public ushort ItemIDSize { get; set; }

        public const int SizeFieldLength = sizeof(ushort);

        public int GetLength()
        {
            return this.GetDataLength() + SizeFieldLength;
        }

        protected abstract int GetDataLength();

        public void WriteTo(BinaryWriter writer, IOptions options)
        {
            writer.Write(this.ItemIDSize);
            this.WriteDataTo(writer, options);
        }

        protected abstract void WriteDataTo(BinaryWriter writer, IOptions options);

        public void Check(List<Exception> errors)
        {
            if (this.ItemIDSize != this.GetDataLength() + SizeFieldLength)
                errors.Add(new ArgumentException(
                    $"{nameof(ItemIDSize)} is not equal to {nameof(GetDataLength)}() + 2", nameof(this.ItemIDSize)));


        }

        protected abstract void CheckData(List<Exception> errors);

        public virtual void Repair()
        {
            this.ItemIDSize = (ushort)(this.GetDataLength() + SizeFieldLength);
        }
    }
}