namespace ShellLink.ItemID
{
    public class TypeIndicatorBasedItemID : ShellItemId
    {
        /// <summary>
        /// Class type indicator (0x1f for Root folder shell item)
        /// </summary>
        public ItemIDTypeIndicator TypeIndicator { get; set; }
    }
}