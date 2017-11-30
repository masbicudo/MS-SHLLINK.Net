using System;
using System.ComponentModel;

namespace ShellLink
{
    /// <summary>
    /// The ShellLinkHeader structure contains identification information, timestamps, and flags that specify
    /// the presence of optional structures, including LinkTargetIDList (section 2.2), LinkInfo (section 2.3),
    /// and StringData (section 2.4).
    /// </summary>
    public sealed class ShellLinkHeader
    {
        public static readonly Guid ShellLinkCLSID = Guid.ParseExact("00021401-0000-0000-C000-000000000046", "D");

        /// <summary>
        /// The size, in bytes, of this structure. This value MUST be 0x0000004C.
        /// </summary>
        public int HeaderSize { get; set; }

        /// <summary>
        /// A class identifier (CLSID). This value MUST be 00021401-0000-0000-C000-000000000046.
        /// </summary>
        public Guid LinkCLSID { get; set; }

        /// <summary>
        /// A LinkFlags structure (section 2.1.1) that specifies information about the shell
        /// link and the presence of optional portions of the structure.
        /// </summary>
        public LinkFlags LinkFlags { get; set; }

        /// <summary>
        /// A FileAttributesFlags structure (section 2.1.2) that specifies information
        /// about the link target.
        /// </summary>
        public FileAttributes FileAttributes { get; set; }

        /// <summary>
        /// A FILETIME structure ([MS-DTYP] section 2.3.3) that specifies the creation
        /// time of the link target in UTC (Coordinated Universal Time). If the value is zero, there is no
        /// creation time set on the link target.
        /// </summary>
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// A FILETIME structure ([MS-DTYP] section 2.3.3) that specifies the access
        /// time of the link target in UTC (Coordinated Universal Time). If the value is zero, there is no access
        /// time set on the link target.
        /// </summary>
        public DateTime? AccessTime { get; set; }

        /// <summary>
        /// A FILETIME structure ([MS-DTYP] section 2.3.3) that specifies the write time
        /// of the link target in UTC (Coordinated Universal Time). If the value is zero, there is no write time
        /// set on the link target.
        /// </summary>
        public DateTime? WriteTime { get; set; }

        /// <summary>
        /// A 32-bit unsigned integer that specifies the size, in bytes, of the link target. If the
        /// link target file is larger than 0xFFFFFFFF, this value specifies the least significant 32 bits of the link
        /// target file size.
        /// </summary>
        public uint FileSize { get; set; }

        /// <summary>
        /// A 32-bit signed integer that specifies the index of an icon within a given icon
        /// location.
        /// </summary>
        public int IconIndex { get; set; }

        /// <summary>
        /// A 32-bit unsigned integer that specifies the expected window state of an
        /// application launched by the link. This value SHOULD be one of the following.
        /// </summary>
        public ShowCommand ShowCommand { get; set; }

        /// <summary>
        /// A HotKeyFlags structure (section 2.1.3) that specifies the keystrokes used to
        /// launch the application referenced by the shortcut key. This value is assigned to
        /// the application after it is launched, so that pressing the key activates that application.
        /// </summary>
        public HotKeyFlags HotKey { get; set; }

        /// <summary>
        /// A value that MUST be zero.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public short Reserved1 { get; set; }

        /// <summary>
        /// A value that MUST be zero.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Reserved2 { get; set; }

        /// <summary>
        /// A value that MUST be zero.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Reserved3 { get; set; }
    }
}