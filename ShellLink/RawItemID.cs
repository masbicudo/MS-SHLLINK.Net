using System.IO;

namespace ShellLink
{
    public sealed class RawItemID : ItemId
    {
        /// <summary>
        /// The shell data source-defined data that specifies an item.
        /// </summary>
        public byte[] Data { get; set; }

        public override int GetDataLength() => this.Data?.Length ?? 0;

        protected override void WriteDataTo(BinaryWriter writer)
        {
            if (this.Data != null)
                writer.Write(this.Data);
        }
    }
}