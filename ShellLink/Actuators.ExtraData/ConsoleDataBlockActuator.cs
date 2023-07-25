using ShellLink.DataObjects;
using ShellLink.DataObjects.ExtraData;
using ShellLink.Internals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShellLink.Actuators.ExtraData
{
    public sealed class ConsoleDataBlockActuator : ExtraDataBlockActuator<ConsoleDataBlock>
    {
        public override int MinBlockSize { get; } = 0x000000CC;
        public override int MaxBlockSize { get; } = 0x000000CC;
        public override int StandardBlockSignature { get; } = unchecked((int)0xA0000002);

        protected override void WriteDataTo(ConsoleDataBlock edb, BinaryWriter writer, IOptions options)
        {
            writer.Write((short)edb.FillAttributes.GetData());
            writer.Write((short)edb.PopupFillAttributes.GetData());

            writer.Write((short)edb.ScreenBufferSizeX);
            writer.Write((short)edb.ScreenBufferSizeY);
            writer.Write((short)edb.WindowSizeX);
            writer.Write((short)edb.WindowSizeY);
            writer.Write((short)edb.WindowOriginX);
            writer.Write((short)edb.WindowOriginY);

            writer.Write((int)edb.Unused1);
            writer.Write((int)edb.Unused2);

            writer.Write((int)edb.FontSize);
            writer.Write((int)edb.FontFamily);
            writer.Write((int)edb.FontWeight);

            writer.WriteFixedSizeString(edb.FaceName, 32, Encoding.Unicode);

            writer.Write((int)edb.CursorSize);
            writer.Write((int)edb.FullScreen);
            writer.Write((int)edb.QuickEdit);
            writer.Write((int)edb.InsertMode);
            writer.Write((int)edb.AutoPosition);
            writer.Write((int)edb.HistoryBufferSize);
            writer.Write((int)edb.NumberOfHistoryBuffers);
            writer.Write((int)edb.HistoryNoDup);

            for (var it = 0; it < 16; it++)
                writer.Write((int)edb.ColorTable[it]);
        }

        protected override bool LoadData(ConsoleDataBlock edb, BinaryReader reader, IOptions options)
        {
            edb.FillAttributes = new ConsoleColors(reader.ReadInt16());
            edb.PopupFillAttributes = new ConsoleColors(reader.ReadInt16());

            edb.ScreenBufferSizeX = reader.ReadInt16();
            edb.ScreenBufferSizeY = reader.ReadInt16();
            edb.WindowSizeX = reader.ReadInt16();
            edb.WindowSizeY = reader.ReadInt16();
            edb.WindowOriginX = reader.ReadInt16();
            edb.WindowOriginY = reader.ReadInt16();

            edb.Unused1 = reader.ReadInt32();
            edb.Unused2 = reader.ReadInt32();

            edb.FontSize = reader.ReadInt32();
            edb.FontFamily = (FontFamily)reader.ReadInt32();
            edb.FontWeight = reader.ReadInt32();

            edb.FaceName = reader.ReadFixedSizeString(32, Encoding.Unicode, ZeroCharBehavior.RemoveTrailing);

            edb.CursorSize = reader.ReadInt32();
            edb.FullScreen = reader.ReadInt32();
            edb.QuickEdit = reader.ReadInt32();
            edb.InsertMode = reader.ReadInt32();
            edb.AutoPosition = reader.ReadInt32();
            edb.HistoryBufferSize = reader.ReadInt32();
            edb.NumberOfHistoryBuffers = reader.ReadInt32();
            edb.HistoryNoDup = reader.ReadInt32();

            for (var it = 0; it < 16; it++)
                edb.ColorTable[it] = reader.ReadInt32();

            return true;
        }

        protected override void CheckData(ConsoleDataBlock edb, List<Exception> errors, ShellLinkObject shellLinkObject)
        {
        }

        protected override void RepairData(ConsoleDataBlock edb)
        {
            // TODO
        }

        public override ExtraDataBlock Read(BinaryReader reader, uint size, int sig, IOptions options)
        {
            throw new NotImplementedException();
        }
    }
}