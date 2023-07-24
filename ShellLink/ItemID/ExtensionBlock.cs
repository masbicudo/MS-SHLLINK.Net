namespace ShellLink.ItemID
{
    public abstract class ExtensionBlock
    {
        public ushort Size { get; internal set; }
        public ushort Version { get; internal set; }
        public uint Signature { get; internal set; }
    }
}