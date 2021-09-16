﻿using ShellLink.Internals;
using System.Collections.Generic;
using System.IO;

namespace ShellLink.ExtraData
{
    public sealed class ExtraDataBlockProvider
    {
        public ExtraDataBlockProvider(IOptions options)
        {
            this.BlockReaders = new List<IExtraDataBlockReader>
                {
                    new ExtraDataBlockReader<ConsoleDataBlock>(),
                    new ExtraDataBlockReader<ConsoleFEDataBlock>(),
                    new ExtraDataBlockReader<DarwinDataBlock>(),
                    new ExtraDataBlockReader<EnvironmentVariableDataBlock>(),
                    new ExtraDataBlockReader<IconEnvironmentDataBlock>(),
                    new ExtraDataBlockReader<KnownFolderDataBlock>(),
                    new ExtraDataBlockReader<PropertyStoreDataBlock>(),
                    new ExtraDataBlockReader<ShimDataBlock>(),
                    new ExtraDataBlockReader<SpecialFolderDataBlock>(),
                    new ExtraDataBlockReader<TrackerDataBlock>(),
                    new ExtraDataBlockReader<VistaAndAboveIDListDataBlock>(),
                };
            this.Options = options;
        }

        public ExtraDataBlockProvider(IExtraDataBlockReader[] blockReaders)
        {
            this.BlockReaders = blockReaders;
        }

        public IReadOnlyCollection<IExtraDataBlockReader> BlockReaders { get; }
        public IOptions Options { get; }

        public ExtraDataBlock Read(BinaryReader reader)
        {
            var size = reader.ReadInt32();

            if (size >= 0 && size < 4)
                return new EmptyExtraDataBlock { BlockSize = size };

            var sig = reader.ReadInt32();

            var buffer = reader.ReadBytes(size - 8);

            foreach (var blockReader in this.BlockReaders)
            {
                var subreader = new BinaryReader(new MemoryStream(buffer));
                var block = blockReader.Read(subreader, size, sig, this.Options);

                // must not be null
                // must read everything in the buffer
                if (block != null && subreader.BaseStream.Position == buffer.LongLength)
                    return block;
            }

            var result = new RawExtraDataBlock
            {
                BlockSize = size,
                BlockSignature = sig,
                Data = buffer,
            };

            return result;
        }
    }
}