using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

        protected override void WriteDataTo(BinaryWriter writer)
        {
            this.PropertyStore.WriteTo(writer);
        }

        protected override bool LoadData(BinaryReader reader)
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

    public static class SerializedPropertyStoreLoader
    {
        public static bool Load([NotNull] this SerializedPropertyStore obj, BinaryReader reader)
        {
            var ok = true;

            obj.StoreSize = reader.ReadInt32();

            while (true)
            {
                var storage = new SerializedPropertyStorage();

                ok &= storage.Load(reader);

                obj.SerializedPropertyStorages.Add(storage);

                if (storage.StorageSize == 0)
                    break;
            }

            return ok;
        }

        public static bool WriteTo([NotNull] this SerializedPropertyStore obj, BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    public static class SerializedPropertyStorageLoader
    {
        public static readonly Guid ByNamePropsGuid = Guid.Parse("D5CDD505-2E9C-101B-9397-08002B2CF9AE");

        public static bool Load([NotNull] this SerializedPropertyStorage obj, BinaryReader reader)
        {
            obj.StorageSize = reader.ReadInt32();

            if (obj.StorageSize == 0)
                return true;

            obj.Version = reader.ReadInt32();
            obj.FormatID = reader.ReadGuid();

            var ok = true;

            while (true)
            {
                // TODO
                // Error tolerance:
                //  To be error tolerant, we will behave as if the binary sequence
                // was a string and we were to apply the following regex:
                //      ^(ByName|ById)*$
                // This means that we will backtrack to try to find alternatives.

                // At this moment what we have is:
                // - No backtracking
                // - Test by name, and if it fails, read by id

                var namedValue = new SerializedPropertyValueByName();
                var ok2 = namedValue.Load(reader);
                SerializedPropertyValue value = namedValue;
                if (!ok2)
                {
                    var identifiedValue = new SerializedPropertyValueById();
                    ok2 = identifiedValue.Load(reader);
                    value = identifiedValue;
                }

                ok &= ok2;

                obj.SerializedPropertyValues.Add(value);

                if (value.ValueSize == 0)
                    break;
            }

            return ok;
        }

        public static bool WriteTo([NotNull] this SerializedPropertyStorage obj, BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }

    public static class SerializedPropertyValueLoader
    {
        public static bool Load([NotNull] this SerializedPropertyValueByName obj, BinaryReader reader)
        {
            obj.ValueSize = reader.ReadInt32();

            if (obj.ValueSize == 0)
                return true;

            obj.NameSize = reader.ReadInt32();

            obj.Reserved = reader.ReadByte();

            var name = reader.ReadFixedSizeString(obj.NameSize, Encoding.Unicode, ZeroCharBehavior.None);
            var hasZeroAtEndOnly = name.IndexOf('\0') == name.Length - 1;
            obj.Name = name.TrimEnd('\0');

            var remainingBytes = obj.ValueSize - 4 - 4 - 1 - obj.NameSize;
            obj.Value = reader.ReadBytes(remainingBytes);

            return hasZeroAtEndOnly;
        }

        public static bool Load([NotNull] this SerializedPropertyValueById obj, BinaryReader reader)
        {
            obj.ValueSize = reader.ReadInt32();

            if (obj.ValueSize == 0)
                return true;

            obj.Id = reader.ReadInt32();

            obj.Reserved = reader.ReadByte();

            var remainingBytes = obj.ValueSize - 4 - 4 - 1;
            obj.Value = reader.ReadBytes(remainingBytes);

            return true;
        }

    }
}