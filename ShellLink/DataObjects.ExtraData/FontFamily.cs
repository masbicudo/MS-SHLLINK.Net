namespace ShellLink.DataObjects.ExtraData
{
    public enum FontFamily
    {
        /// <summary> The font family is unknown. </summary>
        DontCare = 0,
        /// <summary> The font is variable-width with serifs; for example, "Times New Roman". </summary>
        Roman = 0x10,
        /// <summary> The font is variable-width without serifs; for example, "Arial". </summary>
        Swiss = 0x20,
        /// <summary> The font is fixed-width, with or without serifs; for example, "Courier New". </summary>
        Modern = 0x30,
        /// <summary> The font is designed to look like handwriting; for example, "Cursive". </summary>
        Script = 0x40,
        /// <summary> The font is a novelty font; for example, "Old English". </summary>
        Decorative = 0x50,
    }
}