using JetBrains.Annotations;
using ShellLink.Internals;
using ShellLink.PropertyStore;
using System;
using System.IO;

namespace ShellLink.Actuators.ExtraData
{
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
}