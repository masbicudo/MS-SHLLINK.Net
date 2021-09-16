using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using ShellLink.DataObjects;
using ShellLink.Internals;
using ShellLink.PropertyStore;

namespace ShellLink.ExtraData
{
    /// <summary>
    /// A PropertyStoreDataBlock structure specifies a set of properties
    /// that can be used by applications to store extra data in the shell link.
    /// </summary>
    public sealed class PropertyStoreDataBlock : ExtraDataBlock
    {
        /// <summary>
        /// A 32-bit, unsigned integer that specifies the size of the
        /// PropertyStoreDataBlock structure.
        /// This value MUST be greater than or equal to 0x0000000C.
        /// </summary>
        public override int BlockSize { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the signature of the
        /// PropertyStoreDataBlock extra data section.
        /// This value MUST be 0xA0000009.
        /// </summary>
        public override int BlockSignature { get; set; }

        /// <summary>
        /// A serialized property storage structure ([MS-PROPSTORE] section 2.2).
        /// </summary>
        [NotNull]
        public SerializedPropertyStore PropertyStore { get; } = new SerializedPropertyStore();

        protected override int GetDataLength() => 0x0000000C - ExtraDataBlock.SizeAndSigFieldLength;

        public override int GetSignatureValue() => unchecked((int)0xA000000B);

        protected override void WriteDataTo(BinaryWriter writer, IOptions options)
        {
            this.PropertyStore.WriteTo(writer);
        }

        protected override bool LoadData(BinaryReader reader, IOptions options)
        {
            var ok = true;
            ok &= this.PropertyStore.Load(reader);
            return true;
        }

        protected override void CheckData(List<Exception> errors, ShellLinkObject shellLinkObject)
        {
            // TODO: check whether the IDList contains an ItemID at the given offset
        }

        protected override void RepairData()
        {
            // the error cannot be repaired easily
        }
    }
}