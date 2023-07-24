namespace ShellLink.DataObjects.ExtraData
{
    public class RawExtraDataBlock : ExtraDataBlock
    {
        public byte[] Data { get; set; }

        public override uint BlockSize { get; set; }

        public override int BlockSignature { get; set; }
    }
}