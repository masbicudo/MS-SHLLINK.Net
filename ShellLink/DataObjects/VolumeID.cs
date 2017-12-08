using System.IO;

namespace ShellLink.DataObjects
{
    /// <summary>
    /// The VolumeID structure specifies information about the volume that
    /// a link target was on when the link was created. This information
    /// is useful for resolving the link if the file is not found in its
    /// original location.
    /// </summary>
    public sealed class VolumeID
    {
        /// <summary>
        /// A 32-bit, unsigned integer that specifie
        ///  the size, in bytes, of this structure.
        /// This value MUST be greater than 0x00000010.
        /// All offsets specified in this structur
        ///  MUST be less than this value,
        /// and all strings contained in this structur
        ///  MUST fit within the extent defined by this size.
        /// </summary>
        public uint VolumeIDSize { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the
        /// type of drive the link target is stored on.
        /// </summary>
        public DriveType DriveType { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the drive
        /// serial number of the volume the link target is stored on.
        /// </summary>
        public uint DriveSerialNumber { get; set; }

        /// <summary>
        /// A 32-bit, unsigned integer that specifies the location
        /// of a string that contains the volume label of the drive
        /// that the link target is stored on. This value is an offset,
        /// in bytes, from the start of the VolumeID structure to a
        /// NULL-terminated string of characters, defined by the
        /// system default code page. The volume label string is
        /// located in the Data field of this structure.
        /// <para>
        /// If the value of this field is 0x00000014, it MUST be ignored,
        /// and the value of the VolumeLabelOffsetUnicode field
        /// MUST be used to locate the volume label string.
        /// </para>
        /// </summary>
        public uint VolumeLabelOffset { get; set; }

        /// <summary>
        /// An optional, 32-bit, unsigned integer that specifies the
        /// location of a string that contains the volume label of the
        /// drive that the link target is stored on.
        /// This value is an offset, in bytes, from the start of the
        /// VolumeID structure to a NULL-terminated string of Unicode
        /// characters. The volume label string is located in the
        /// Data field of this structure.
        /// <para>
        /// If the value of the VolumeLabelOffset field is
        /// not 0x00000014, this field MUST be ignored, and the value of the
        /// VolumeLabelOffset field MUST be used to locate the
        /// volume label string.
        /// </para>
        /// </summary>
        public uint? VolumeLabelOffsetUnicode { get; set; }

        /// <summary>
        /// A buffer of data that contains the volume label of the
        /// drive as a string defined by the
        /// system default code page or Unicode characters,
        /// as specified by preceding fields.
        /// </summary>
        public string Data { get; set; }
    }
}