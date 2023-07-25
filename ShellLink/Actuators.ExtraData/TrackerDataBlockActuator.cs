using ShellLink.DataObjects;
using ShellLink.DataObjects.ExtraData;
using ShellLink.Internals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShellLink.Actuators.ExtraData
{
    public sealed class TrackerDataBlockActuator : ExtraDataBlockActuator<TrackerDataBlock>
    {
        public override int MinBlockSize { get; } = 0x00000060;
        public override int MaxBlockSize { get; } = 0x00000060;
        public override int StandardBlockSignature { get; } = unchecked((int)0xA0000003);

        protected override void WriteDataTo(TrackerDataBlock edb, BinaryWriter writer, IOptions options)
        {
            writer.Write(edb.Length);
            writer.Write(edb.Version);

            // ref: https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-dltw/240143d3-366d-4530-8828-1e82e98b68d7
            writer.WriteNullTerminatedString(edb.MachineID, Encoding.ASCII, 16, 16);

            // ref: https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-dltw/80cbf3f6-964d-456a-b08e-6f20c7c86921
            writer.WriteGuid(edb.Droid.VolumeId);
            writer.WriteGuid(edb.Droid.ObjectId);
            writer.WriteGuid(edb.DroidBirth.VolumeId);
            writer.WriteGuid(edb.DroidBirth.ObjectId);
        }

        protected override bool LoadData(TrackerDataBlock edb, BinaryReader reader, IOptions options)
        {
            edb.Length = reader.ReadInt32();
            edb.Version = reader.ReadInt32();

            // ref: https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-dltw/240143d3-366d-4530-8828-1e82e98b68d7
            edb.MachineID = reader.ReadFixedSizeString(16, Encoding.ASCII, ZeroCharBehavior.SplitFirst);

            // ref: https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-dltw/80cbf3f6-964d-456a-b08e-6f20c7c86921
            edb.Droid.VolumeId = reader.ReadGuid();
            edb.Droid.ObjectId = reader.ReadGuid();
            edb.DroidBirth.VolumeId = reader.ReadGuid();
            edb.DroidBirth.ObjectId = reader.ReadGuid();
            return true;
        }

        protected override void CheckData(TrackerDataBlock edb, List<Exception> errors, ShellLinkObject shellLinkObject)
        {
            if (edb.Version != 0x00000000)
                errors.Add(new ArgumentException(
                    "Version field must be 0x00000000"));

            if (edb.Length != 0x00000058)
                errors.Add(new ArgumentException(
                    "Length field must be 0x00000058"));
        }

        protected override void RepairData(TrackerDataBlock edb)
        {
            // TODO
        }

        public override ExtraDataBlock Read(BinaryReader reader, uint size, int sig, IOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
