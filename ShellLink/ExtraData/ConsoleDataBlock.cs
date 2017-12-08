using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ShellLink.DataObjects;
using ShellLink.Internals;

namespace ShellLink.ExtraData
{
    /// <summary>
    /// The ConsoleDataBlock structure specifies the display
    /// settings to use when a link target specifies an
    /// application that is run in a console window.
    /// </summary>
    public sealed class ConsoleDataBlock : ExtraDataBlock
    {
        /// <summary>
        /// A 32-bit, unsigned integer that specifies the size of the
        /// ConsoleDataBlock structure. This value MUST be 0x000000CC.
        /// </summary>
        public override int BlockSize { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the signature 
        /// of the ConsoleDataBlock extra data section.
        /// This value MUST be 0xA0000002.
        /// </summary>
        public override int BlockSignature { get; set; }

        /// <summary>
        /// A 16-bit, unsigned integer that specifies the 
        /// fill attributes that control the foreground 
        /// and background text colors in the console window. 
        /// The following bit definitions can be combined 
        /// to specify 16 different values each for the 
        /// foreground and background colors.
        /// </summary>
        public ConsoleColors FillAttributes { get; set; }

        /// <summary>
        /// A 16-bit, unsigned integer that specifies the 
        /// fill attributes that control the foreground 
        /// and background text color in the console window popup.
        /// The values are the same as for the FillAttributes field.
        /// </summary>
        public ConsoleColors PopupFillAttributes { get; set; }

        /// <summary>
        /// A 16-bit, signed integer that specifies the horizontal 
        /// size (X axis), in characters, of the console window buffer.
        /// </summary>
        public short ScreenBufferSizeX { get; set; }

        /// <summary>
        /// A 16-bit, signed integer that specifies the vertical 
        /// size (Y axis), in characters, of the console window buffer.
        /// </summary>
        public short ScreenBufferSizeY { get; set; }

        /// <summary>
        /// A 16-bit, signed integer that specifies the horizontal 
        /// size (X axis), in characters, of the console window.
        /// </summary>
        public short WindowSizeX { get; set; }

        /// <summary>
        /// A 16-bit, signed integer that specifies the vertical 
        /// size (Y axis), in characters, of the console window.
        /// </summary>
        public short WindowSizeY { get; set; }

        /// <summary>
        /// A 16-bit, signed integer that specifies the horizontal 
        /// coordinate (X axis), in pixels, of the console window origin.
        /// </summary>
        public short WindowOriginX { get; set; }

        /// <summary>
        /// A 16-bit, signed integer that specifies the vertical 
        /// coordinate (Y axis), in pixels, of the console window origin.
        /// </summary>
        public short WindowOriginY { get; set; }

        /// <summary>
        /// A value that is undefined and MUST be ignored.
        /// </summary>
        public int Unused1 { get; set; }

        /// <summary>
        /// A value that is undefined and MUST be ignored.
        /// </summary>
        public int Unused2 { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the size, 
        /// in pixels, of the font used in the console window.
        /// </summary>
        public int FontSize { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the family 
        /// of the font used in the console window.
        /// This value MUST be one from the following.
        /// <para>
        /// Value Meaning:
        /// FF_DONTCARE 0x0000: The font family is unknown.
        /// FF_ROMAN 0x0010: The font is variable-width with serifs; for example, "Times New Roman".
        /// FF_SWISS 0x0020: The font is variable-width without serifs; for example, "Arial".
        /// FF_MODERN 0x0030: The font is fixed-width, with or without serifs; for example, "Courier New".
        /// FF_SCRIPT 0x0040: The font is designed to look like handwriting; for example, "Cursive".
        /// FF_DECORATIVE 0x0050: The font is a novelty font; for example, "Old English".
        /// </para>
        /// </summary>
        public FontFamily FontFamily { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the 
        /// stroke weight of the font used in the console window.
        /// <para>
        /// Value Meaning:
        /// 700 ≤ value: A bold font.
        /// value &lt; 700: A regular-weight font.
        /// </para>
        /// </summary>
        public int FontWeight { get; set; }

        /// <summary>
        /// A 32-character Unicode string that specifies the 
        /// face name of the font used in the console window.
        /// </summary>
        public string FaceName { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the 
        /// size of the cursor, in pixels, used in the console window.
        /// <para>
        /// Value Meaning:
        /// value ≤ 25: A small cursor.
        /// 26 — 50: A medium cursor.
        /// 51 — 100: A large cursor.
        /// </para>
        /// </summary>
        public int CursorSize { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies whether 
        /// to open the console window in full-screen mode.
        /// <para>
        /// Value Meaning:
        /// 0 = Full-screen mode is off.
        /// other = Full-screen mode is on.
        /// </para>
        /// </summary>
        public int FullScreen { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies whether 
        /// to open the console window in QuikEdit mode. 
        /// In QuickEdit mode, the mouse can be used to cut, 
        /// copy, and paste text in the console window.
        /// <para>
        /// Value Meaning:
        /// 0 = QuikEdit mode is off.
        /// other = QuikEdit mode is on.
        /// </para>
        /// </summary>
        public int QuickEdit { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies insert 
        /// mode in the console window.
        /// <para>
        /// Value Meaning:
        /// 0 = Insert mode is disabled.
        /// other = Insert mode is enabled.
        /// </para>
        /// </summary>
        public int InsertMode { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies 
        /// auto-position mode of the console window.
        /// <para>
        /// Value Meaning:
        /// 0 = The values of the WindowOriginX and WindowOriginY fields are used to position the console window.
        /// other = The console window is positioned automatically.
        /// </para>
        /// </summary>
        public int AutoPosition { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the size, 
        /// in characters, of the buffer that is used to store 
        /// a history of user input into the console window.
        /// </summary>
        public int HistoryBufferSize { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the 
        /// number of history buffers to use.
        /// </summary>
        public int NumberOfHistoryBuffers { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies whether 
        /// to remove duplicates in the history buffer.
        /// <para>
        /// Value Meaning:
        /// 0 = Duplicates are not allowed.
        /// other = Duplicates are allowed.
        /// </para>
        /// </summary>
        public int HistoryNoDup { get; set; }

        /// <summary>
        /// A table of 16 32-bit, unsigned integers specifying 
        /// the RGB colors that are used for text in the console window. 
        /// The values of the fill attribute fields FillAttributes 
        /// and PopupFillAttributes are used as indexes into this 
        /// table to specify the final foreground and background 
        /// color for a character.
        /// </summary>
        public int[] ColorTable { get; } = new int[16];

        protected override int GetDataLength() => 0x000000CC - ExtraDataBlock.SizeAndSigFieldLength;

        public override int GetSignatureValue() => unchecked((int)0xA0000002);

        protected override void WriteDataTo(BinaryWriter writer)
        {
            writer.Write((short)this.FillAttributes.GetData());
            writer.Write((short)this.PopupFillAttributes.GetData());

            writer.Write((short)this.ScreenBufferSizeX);
            writer.Write((short)this.ScreenBufferSizeY);
            writer.Write((short)this.WindowSizeX);
            writer.Write((short)this.WindowSizeY);
            writer.Write((short)this.WindowOriginX);
            writer.Write((short)this.WindowOriginY);

            writer.Write((int)this.Unused1);
            writer.Write((int)this.Unused2);

            writer.Write((int)this.FontSize);
            writer.Write((int)this.FontFamily);
            writer.Write((int)this.FontWeight);

            writer.WriteFixedSizeString(this.FaceName, 32, Encoding.Unicode);

            writer.Write((int)this.CursorSize);
            writer.Write((int)this.FullScreen);
            writer.Write((int)this.QuickEdit);
            writer.Write((int)this.InsertMode);
            writer.Write((int)this.AutoPosition);
            writer.Write((int)this.HistoryBufferSize);
            writer.Write((int)this.NumberOfHistoryBuffers);
            writer.Write((int)this.HistoryNoDup);

            for (var it = 0; it < 16; it++)
                writer.Write((int)this.ColorTable[it]);
        }

        protected override bool LoadData(BinaryReader reader)
        {
            this.FillAttributes = new ConsoleColors(reader.ReadInt16());
            this.PopupFillAttributes = new ConsoleColors(reader.ReadInt16());

            this.ScreenBufferSizeX = reader.ReadInt16();
            this.ScreenBufferSizeY = reader.ReadInt16();
            this.WindowSizeX = reader.ReadInt16();
            this.WindowSizeY = reader.ReadInt16();
            this.WindowOriginX = reader.ReadInt16();
            this.WindowOriginY = reader.ReadInt16();

            this.Unused1 = reader.ReadInt32();
            this.Unused2 = reader.ReadInt32();

            this.FontSize = reader.ReadInt32();
            this.FontFamily = (FontFamily)reader.ReadInt32();
            this.FontWeight = reader.ReadInt32();

            this.FaceName = reader.ReadFixedSizeString(32, Encoding.Unicode);

            this.CursorSize = reader.ReadInt32();
            this.FullScreen = reader.ReadInt32();
            this.QuickEdit = reader.ReadInt32();
            this.InsertMode = reader.ReadInt32();
            this.AutoPosition = reader.ReadInt32();
            this.HistoryBufferSize = reader.ReadInt32();
            this.NumberOfHistoryBuffers = reader.ReadInt32();
            this.HistoryNoDup = reader.ReadInt32();

            for (var it = 0; it < 16; it++)
                this.ColorTable[it] = reader.ReadInt32();

            return true;
        }

        protected override void CheckData(List<Exception> errors, ShellLinkObject shellLinkObject)
        {
        }

        protected override void RepairData()
        {
            // TODO
        }
    }
}