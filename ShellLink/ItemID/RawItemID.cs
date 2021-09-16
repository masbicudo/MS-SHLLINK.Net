using ShellLink.Internals;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShellLink
{
    public sealed class RawItemID : ItemID
    {
        /// <summary>
        /// The shell data source-defined data that specifies an item.
        /// </summary>
        public byte[] Data { get; set; }

        protected override int GetDataLength() => this.Data?.Length ?? 0;

        protected override void WriteDataTo(BinaryWriter writer, IOptions options)
        {
            if (this.Data != null)
                writer.Write(this.Data);
        }

        protected override void CheckData(List<Exception> errors)
        {
        }
    }
}