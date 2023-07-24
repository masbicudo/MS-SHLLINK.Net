namespace ShellLink
{
    public enum ItemIDTypeIndicator
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown00 = 0x00,

        /// <summary>
        /// Unknown
        /// </summary>
        Unknown01 = 0x01,

        /// <summary>
        /// Type 0x08 (with size of 6) is alias?
        /// </summary>
        Unknown08 = 0x08,

        /// <summary>
        /// Type 0x0c is alias?
        /// </summary>
        Unknown0c = 0x0c,

        /// <summary>
        /// Unknown
        /// </summary>
        Unknown17 = 0x17,

        /// <summary>
        /// Not seen in wild but reason to believe it exists.
        /// </summary>
        /// <remarks>
        /// What is the relationship between the root (first) shell item (0x1f/0x1e?) and the other shell items?
        /// </remarks>
        CLSID_ShellDesktop = 0x1e,

        /// <summary>
        /// Root folder shell item
        /// </summary>
        /// <remarks>
        /// CLSID_ShellDesktop
        /// Likely IshellFolder interface?
        /// What is the relationship between the root (first) shell item (0x1f/0x1e?) and the other shell items?
        /// </remarks>
        RootFolder = 0x1f,

        /// <summary>
        /// Volume shell item
        /// See section: Volume shell item
        /// <see cref="https://github.com/libyal/libfwsi/blob/main/documentation/Windows%20Shell%20Item%20format.asciidoc#volume_shell_item"/>
        /// </summary>
        CLSID_MyComputer = 0x20,

        /// <summary>
        /// File entry shell item
        /// See section: File entry shell item
        /// <see cref="https://github.com/libyal/libfwsi/blob/main/documentation/Windows%20Shell%20Item%20format.asciidoc#file_entry_shell_item"/>
        /// </summary>
        CLSID_ShellFSFolder = 0x30,

        /// <summary>
        /// 0x3a Name space object? Link blessing? My Computer (CRegFolder)?
        /// </summary>
        Unknown3a = 0x3a,

        /// <summary>
        /// Network location shell item
        /// See section: Network location shell item
        /// <see cref="https://github.com/libyal/libfwsi/blob/main/documentation/Windows%20Shell%20Item%20format.asciidoc#network_location_shell_item"/>
        /// </summary>
        CLSID_NetworkRoot = 0x40,

        /// <summary>
        /// Network location shell item
        /// See section: Network location shell item
        /// <see cref="https://github.com/libyal/libfwsi/blob/main/documentation/Windows%20Shell%20Item%20format.asciidoc#network_location_shell_item"/>
        /// </summary>
        CLSID_NetworkPlaces = 0x40,

        /// <summary>
        /// Compressed folder shell item
        /// See section: Compressed folder shell item
        /// <see cref="https://github.com/libyal/libfwsi/blob/main/documentation/Windows%20Shell%20Item%20format.asciidoc#compressed_folder_shell_item"/>
        /// </summary>
        CompressedFolder = 0x52,

        /// <summary>
        /// URI shell item
        /// </summary>
        CLSID_Internet = 0x61,

        /// <summary>
        /// Not seen in wild but reason to believe it exists.
        /// item has no item data at offset 0x04
        /// </summary>
        ControlPanel = 0x70,

        /// <summary>
        /// Control Panel shell item
        /// </summary>
        ControlPanelTasks = 0x71,

        /// <summary>
        /// Printers
        /// Not seen in wild but reason to believe it exists.
        /// </summary>
        Printers = 0x72,

        /// <summary>
        /// Not seen in wild but reason to believe it exists.
        /// </summary>
        CommonPlacesFolder = 0x73,

        /// <summary>
        /// Unknown
        /// Only seen as delegate item
        /// </summary>
        UsersFilesFolder = 0x74,

        /// <summary>
        /// Unknown
        /// </summary>
        Unknown76 = 0x76,

        /// <summary>
        /// 0x7b extension?
        /// </summary>
        Unknown7b = 0x7b,

        /// <summary>
        /// Unknown – different meaning per class type indicator?
        /// </summary>
        Unknown80 = 0x80,

        /// <summary>
        /// Unknown
        /// </summary>
        Unknownff = 0xff,
    }
}