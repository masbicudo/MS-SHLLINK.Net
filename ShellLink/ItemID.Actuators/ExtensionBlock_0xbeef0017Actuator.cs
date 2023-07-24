using ShellLink.Parameters;
using System;
using System.Linq;

namespace ShellLink.ItemID.Actuators
{
    public sealed class ExtensionBlock_0xbeef0017Actuator : ExtensionBlockActuator,
        IActuator<ExtensionBlock_0xbeef0017>
    {
        public static readonly uint Signature = 0xbeef0017;
        public static readonly ushort ExpectedSize = 70;
        public static readonly ushort ExpectedVersion = 1;
        public bool Check(CheckParams<ExtensionBlock_0xbeef0017> arguments)
        {
            var baseArguments = arguments.ConvertTo<ExtensionBlock>();
            if (!base.Check(baseArguments)) return false;
            if (arguments.item.Size != ExpectedSize)
                arguments.errors.Add(new Exception($"invalid size value ({arguments.item.Size}), expected {ExpectedSize}"));
            if (arguments.item.Version != 1)
                arguments.errors.Add(new Exception($"invalid version value ({arguments.item.Version}), expected {ExpectedVersion}"));
            if (arguments.item.Signature != Signature)
                arguments.errors.Add(new Exception($"invalid version value ({arguments.item.Signature}), expected {Signature}"));
            if (arguments.item.Unknown1 != 0)
                arguments.errors.Add(new Exception("unknown field 1 should be zero"));
            if (arguments.item.Unknown7 != 0)
                arguments.errors.Add(new Exception("unknown field 7 should be zero"));
            if (!Enumerable.SequenceEqual(arguments.item.Unknown9, new byte[24]))
                arguments.errors.Add(new Exception("unknown field 9 should be zero"));
            return true;
        }

        public int GetLength(LengthParams<ExtensionBlock_0xbeef0017> arguments)
        {
            var baseArguments = arguments.ConvertTo<ExtensionBlock>();
            var totalSize = base.GetLength(baseArguments);
            totalSize += (7 * 4) + (1 * 8) + (arguments.item.Unknown9.Length) + (1 * 16);
            return totalSize;
        }

        public bool Read(ReadParams<ExtensionBlock_0xbeef0017> arguments)
        {
            var baseArguments = arguments.ConvertTo<ExtensionBlock>();
            if (!base.Read(baseArguments)) return false;
            arguments.item.Unknown1 = arguments.reader.ReadInt32();
            arguments.item.Unknown2 = arguments.reader.ReadInt32();
            arguments.item.Unknown3 = arguments.reader.ReadInt32();
            arguments.item.Unknown4 = arguments.reader.ReadInt32();
            arguments.item.Unknown5 = arguments.reader.ReadInt32();
            arguments.item.Unknown6 = arguments.reader.ReadInt32();
            arguments.item.Unknown7 = arguments.reader.ReadInt64();
            arguments.item.Unknown8 = arguments.reader.ReadInt32();
            arguments.item.Unknown9 = arguments.reader.ReadBytes(24);
            arguments.item.FirstExtensionBlockVersionOffset = arguments.reader.ReadUInt16();
            return true;
        }

        public bool Repair(RepairParams<ExtensionBlock_0xbeef0017> arguments)
        {
            var baseArguments = arguments.ConvertTo<ExtensionBlock>();
            if (!base.Repair(baseArguments)) return false;
            if (arguments.item == null) return false;
            arguments.item.Size = ExpectedSize;
            arguments.item.Version = ExpectedVersion;
            arguments.item.Signature = Signature;
            arguments.item.Unknown1 = 0;
            arguments.item.Unknown7 = 0;
            arguments.item.Unknown9 = new byte[24];
            return true;
        }

        public bool Write(WriteParams<ExtensionBlock_0xbeef0017> arguments)
        {
            var baseArguments = arguments.ConvertTo<ExtensionBlock>();
            if (!base.Write(baseArguments)) return false;
            arguments.writer.Write(arguments.item.Unknown1);
            arguments.writer.Write(arguments.item.Unknown2);
            arguments.writer.Write(arguments.item.Unknown3);
            arguments.writer.Write(arguments.item.Unknown4);
            arguments.writer.Write(arguments.item.Unknown5);
            arguments.writer.Write(arguments.item.Unknown6);
            arguments.writer.Write(arguments.item.Unknown7);
            arguments.writer.Write(arguments.item.Unknown8);
            arguments.writer.Write(arguments.item.Unknown9);
            arguments.writer.Write(arguments.item.FirstExtensionBlockVersionOffset);
            return true;
        }
    }
}