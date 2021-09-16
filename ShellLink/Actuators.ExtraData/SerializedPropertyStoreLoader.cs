using JetBrains.Annotations;
using ShellLink.PropertyStore;
using System;
using System.IO;

namespace ShellLink.Actuators.ExtraData
{
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
}