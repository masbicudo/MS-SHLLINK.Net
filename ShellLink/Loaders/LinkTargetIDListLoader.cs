﻿using System.IO;
using JetBrains.Annotations;
using ShellLink.DataObjects;

namespace ShellLink.Loaders
{
    public static class LinkTargetIDListLoader
    {
        public static bool Load([NotNull] this LinkTargetIDList obj, BinaryReader reader, ItemIDProvider provider)
        {
            obj.IDListSize = reader.ReadUInt16();

            while (true)
            {
                var itemid = provider.Read(reader);

                if (itemid == null)
                    break;

                obj.IDList.ItemIDList.Add(itemid);
            }

            return true;
        }
    }
}