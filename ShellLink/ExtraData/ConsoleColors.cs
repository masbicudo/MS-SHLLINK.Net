using System;

namespace ShellLink.ExtraData
{
    public struct ConsoleColors
    {
        public ConsoleColor Foreground;
        public ConsoleColor Background;

        public ConsoleColors(short data)
        {
            this.Foreground = (ConsoleColor)((data >> 0) & 0xF);
            this.Background = (ConsoleColor)((data >> 4) & 0xF);
        }

        public short GetData()
        {
            return (short)(
                (((short)this.Foreground << 0) & 0xF) +
                (((short)this.Background << 4) & 0xF));
        }
    }
}