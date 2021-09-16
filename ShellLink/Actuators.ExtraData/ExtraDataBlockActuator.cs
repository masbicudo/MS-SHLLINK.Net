using ShellLink.DataObjects.ExtraData;
using ShellLink.Internals;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShellLink.Actuators.ExtraData
{
    public abstract class ExtraDataBlockActuator
    {
        public const int SizeFieldLength = sizeof(uint);
        public const int SizeAndSigFieldLength = sizeof(uint) * 2;
        public int MinDataLength { get => this.MinBlockSize - SizeAndSigFieldLength; }
        public int MaxDataLength { get => this.MaxBlockSize - SizeAndSigFieldLength; }

        /// <summary>
        /// Gets the minimum value of the BlockSize property.
        /// </summary>
        public abstract int MinBlockSize { get; }

        /// <summary>
        /// Gets the maximum value of the BlockSize property.
        /// </summary>
        public abstract int MaxBlockSize { get; }

        /// <summary>
        /// Gets the value the should be assigned to the BlockSignature property
        /// to properly identify the extra data block of this type.
        /// </summary>
        public abstract int StandardBlockSignature { get; }

        protected static void CheckString(List<Exception> errors, string str, int maxsize, string fieldName)
        {
            if (str.Length >= maxsize)
                errors.Add(new ArgumentException(
                    $"Maximum size of {fieldName} is {maxsize - 1} chars.",
                    fieldName));

            if (str.IndexOf('\0') >= 0)
                errors.Add(new ArgumentException(
                    $"{fieldName} must not contain '\\0' characters.",
                    fieldName));
        }

        public abstract ExtraDataBlock Read(BinaryReader reader, int size, int sig, IOptions options);
    }
}