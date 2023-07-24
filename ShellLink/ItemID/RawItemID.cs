namespace ShellLink.ItemID
{
    public sealed class RawItemID : ShellItemId
    {
        /// <summary>
        /// The shell data source-defined data that specifies an item.
        /// </summary>
        public byte[] Data { get; set; }
    }
}