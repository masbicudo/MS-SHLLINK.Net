using ShellLink.Actuators.ExtraData;
using ShellLink.DataObjects.ExtraData;
using ShellLink.Internals;
using System.Collections.Generic;
using System.IO;

namespace ShellLink.Actuators
{
    public sealed class ExtraDataBlockProvider
    {
        public static IReadOnlyCollection<ExtraDataBlockActuator> DefaultDataBlockActuators { get; set; }
            = new List<ExtraDataBlockActuator>
                {
                    new ConsoleDataBlockActuator(),
                    new ConsoleFEDataBlockActuator(),
                    new DarwinDataBlockActuator(),
                    new EnvironmentVariableDataBlockActuator(),
                    new IconEnvironmentDataBlockActuator(),
                    new KnownFolderDataBlockActuator(),
                    new PropertyStoreDataBlockActuator(),
                    new ShimDataBlockActuator(),
                    new SpecialFolderDataBlockActuator(),
                    new TrackerDataBlockActuator(),
                    new VistaAndAboveIDListDataBlockActuator(),
                };

        public ExtraDataBlockProvider(IOptions options) :
            this(DefaultDataBlockActuators, options)
        {
        }

        public ExtraDataBlockProvider(
                IReadOnlyCollection<ExtraDataBlockActuator> blockReaders,
                IOptions options
            )
        {
            this.DataBlockActuators = blockReaders;
            this.Options = options;
        }

        public IReadOnlyCollection<ExtraDataBlockActuator> DataBlockActuators { get; }
        public IOptions Options { get; }

        public ExtraDataBlock Read(BinaryReader reader)
        {
            var size = reader.ReadInt32();

            if (size >= 0 && size < ExtraDataBlockActuator.SizeFieldLength)
                // TODO: must decide if NullExtraDataBlock.BlockSize accepts only 0 or 0 up to 4
                return new NullExtraDataBlock { BlockSize = size };

            var sig = reader.ReadInt32();

            var buffer = reader.ReadBytes(size - ExtraDataBlockActuator.SizeAndSigFieldLength);

            foreach (var blockActuator in this.DataBlockActuators)
            {
                var subreader = new BinaryReader(new MemoryStream(buffer));
                var block = blockActuator.Read(subreader, size, sig, this.Options);

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