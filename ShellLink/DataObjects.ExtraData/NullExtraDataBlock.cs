using System;

namespace ShellLink.DataObjects.ExtraData
{
    public sealed class NullExtraDataBlock : ExtraDataBlock
    {
        public override int BlockSize
        {
            get => 0;
            set
            {
                if (value != 0)
                    throw BlockSizeError();
            }
        }

        public override int BlockSignature
        {
            get => throw BlockSignatureError();
            set => throw BlockSignatureError();
        }

        internal static InvalidOperationException BlockSignatureError()
            => new InvalidOperationException(
                $"{nameof(NullExtraDataBlock)} has a {nameof(BlockSize)} of zero." +
                $" There is no {nameof(BlockSignature)} in this case.");

        internal static InvalidOperationException BlockSizeError()
            => new InvalidOperationException(
                $"{nameof(NullExtraDataBlock)} has a {nameof(BlockSize)} of zero." +
                $" Cannot set it to a different value." +
                $" To avoid this error, never change {nameof(BlockSize)} property if it is zero.");

        internal static InvalidOperationException BlockDataError()
            => new InvalidOperationException(
                $"{nameof(NullExtraDataBlock)} has a {nameof(BlockSize)} of zero." +
                $" There is no data in this object." +
                $" To avoid this error, check if {nameof(BlockSize)} property is zero.");
    }
}