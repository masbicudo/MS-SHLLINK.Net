using System;
using System.Collections.Generic;
using System.IO;
using ShellLink.DataObjects;
using ShellLink.Internals;

namespace ShellLink.ExtraData
{
    /// <summary>
    /// The VistaAndAboveIDListDataBlock structure specifies
    /// an alternate IDList that can be used instead of the
    /// LinkTargetIDList structure (section 2.2) on platforms that support it.<5>
    /// </summary>
    public sealed class VistaAndAboveIDListDataBlock : ExtraDataBlock
    {
        /// <summary>
        /// A 32-bit, unsigned integer that specifies
        /// the size of the VistaAndAboveIDListDataBlock structure.
        /// This value MUST be greater than or equal to 0x0000000A.
        /// </summary>
        public override int BlockSize { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies
        /// the signature of the VistaAndAboveIDListDataBlock extra data section.
        /// This value MUST be 0xA000000C.
        /// </summary>
        public override int BlockSignature { get; set; }

        /// <summary>
        /// An IDList structure (section 2.2.1).
        /// </summary>
        public IDList IDList { get; } = new IDList();

        protected override int GetDataLength() => 0x0000000A - ExtraDataBlock.SizeAndSigFieldLength;

        public override int GetSignatureValue() => unchecked((int)0xA000000C);

        protected override void WriteDataTo(BinaryWriter writer, IOptions options)
        {
            foreach(var itemId in this.IDList.ItemIDList)
            {
                itemId.WriteTo(writer, options);
            }
        }

        protected override bool LoadData(BinaryReader reader, IOptions options)
        {
            var provider = options.Get<ItemIDProvider>();
            while (true)
            {
                var itemid = provider.Read(reader);

                if (itemid == null)
                    break;

                this.IDList.ItemIDList.Add(itemid);
            }
            return true;
        }

        protected override void CheckData(List<Exception> errors, ShellLinkObject shellLinkObject)
        {
        }

        protected override void RepairData()
        {
        }
    }
}