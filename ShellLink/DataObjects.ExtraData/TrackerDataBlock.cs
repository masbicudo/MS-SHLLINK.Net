using ShellLink.DLTW.DataObjects;

namespace ShellLink.DataObjects.ExtraData
{
    /// <summary>
    /// The TrackerDataBlock structure specifies
    /// data that can be used to resolve a link target
    /// if it is not found in its original location when
    /// the link is resolved.
    /// This data is passed to the Link Tracking service
    /// [MS-DLTW] to find the link target.
    /// <br/>
    /// <seealso href="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-dltw/fc649f0e-871a-431a-88b5-d5b2f80e9cc9">
    /// [MS-DLTW]: Distributed Link Tracking: Workstation Protocol
    /// </seealso>
    /// </summary>
    public sealed class TrackerDataBlock : ExtraDataBlock
    {
        /// <summary>
        /// A 32-bit, unsigned integer that specifies
        /// the size of the TrackerDataBlock structure.
        /// This value MUST be 0x00000060.
        /// </summary>
        public override uint BlockSize { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies
        /// the signature of the TrackerDataBlock extra data section.
        /// This value MUST be 0xA0000003.
        /// </summary>
        public override int BlockSignature { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies
        /// the size of the rest of the TrackerDataBlock structure,
        /// including this Length field.
        /// This value MUST be 0x00000058.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer.
        /// This value MUST be 0x00000000.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// A NULL–terminated character string,
        /// as defined by the system default code page,
        /// which specifies the NetBIOS name of the machine
        /// where the link target was last known to reside.
        /// </summary>
        public string MachineID { get; set; }

        /// <summary>
        /// Two values in GUID packet representation
        /// ([MS-DTYP] section 2.3.4.2) that are used to
        /// find the link target with the Link Tracking service,
        /// as described in [MS-DLTW].
        /// </summary>
        public DomainRelativeObjId Droid { get; } = new DomainRelativeObjId();

        /// <summary>
        /// Two values in GUID packet representation
        /// that are used to find the link target
        /// with the Link Tracking service
        /// </summary>
        public DomainRelativeObjId DroidBirth { get; } = new DomainRelativeObjId();
    }
}
