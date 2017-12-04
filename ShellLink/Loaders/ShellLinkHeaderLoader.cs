using System.IO;
using JetBrains.Annotations;
using ShellLink.DataObjects;
using ShellLink.Internals;
using FileAttributes = ShellLink.DataObjects.FileAttributes;

namespace ShellLink.Loaders
{
    public static class ShellLinkHeaderLoader
    {
        public static bool Load([NotNull] this ShellLinkHeader obj, BinaryReader reader)
        {
            obj.HeaderSize = reader.ReadInt32();
            obj.LinkCLSID = reader.ReadGuid();
            obj.LinkFlags = (LinkFlags)reader.ReadInt32();
            obj.FileAttributes = (FileAttributes)reader.ReadInt32();
            obj.CreationTime = reader.ReadDateTime();
            obj.AccessTime = reader.ReadDateTime();
            obj.WriteTime = reader.ReadDateTime();
            obj.FileSize = reader.ReadUInt32();
            obj.IconIndex = reader.ReadInt32();
            obj.ShowCommand = (ShowCommand)reader.ReadInt32();
            obj.HotKey = (HotKeyFlags)reader.ReadInt16();

            obj.Reserved1 = reader.ReadInt16();
            obj.Reserved2 = reader.ReadInt32();
            obj.Reserved3 = reader.ReadInt32();

            return true;
        }
    }
}