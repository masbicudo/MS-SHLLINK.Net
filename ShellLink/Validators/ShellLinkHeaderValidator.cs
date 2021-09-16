using ShellLink.DataObjects;
using ShellLink.DataObjects.Enums;
using ShellLink.Internals;
using System;
using System.Collections.Generic;

namespace ShellLink.Validators
{
    public static class ShellLinkHeaderValidator
    {

        private static readonly Error<ShellLinkHeader>[] allChecks =
        {
            ReservedFieldsNotZero.Instance,
            FileAttributesReservedFlagsAreSet.Instance,
            FileAttributesZeroFlags.Instance,
            FileAttributesNormalFlag.Instance,
            WrongLinkFlags.Instance,
            WrongLinkCLSID.Instance,
            WrongHeaderSize.Instance,
        };

        public static void Check(this ShellLinkHeader obj, List<Exception> errors)
        {
            foreach (var check in allChecks)
                if (check.Check(obj))
                    errors.Add(check.Exception);
        }

        public static bool Repair(this ShellLinkHeader obj)
        {
            bool complete = true;
            foreach (var check in allChecks)
                if (check.Check(obj))
                {
                    if (check.Fixes?.Length == 1)
                        check.Fixes[0].Invoke(obj);
                    else
                        complete = false;
                }

            return complete;
        }

        public sealed class ReservedFieldsNotZero : Error<ShellLinkHeader>
        {
            private ReservedFieldsNotZero() { }

            public static ReservedFieldsNotZero Instance = new ReservedFieldsNotZero();
            public static IAction<ShellLinkHeader>[] FixList = { new SetValueFix() };

            public override IAction<ShellLinkHeader>[] Fixes => FixList;

            public override Exception Exception => new ArgumentOutOfRangeException(
                $"Reserved fields must be zero");

            public override bool Check(ShellLinkHeader header) => header.Reserved1 != 0 || header.Reserved2 != 0 || header.Reserved3 != 0;

            public class SetValueFix : IAction<ShellLinkHeader>
            {
                public void Invoke(ShellLinkHeader header)
                {
                    header.Reserved1 = 0;
                    header.Reserved2 = 0;
                    header.Reserved3 = 0;
                }
            }
        }

        public sealed class FileAttributesReservedFlagsAreSet : Error<ShellLinkHeader>
        {
            private FileAttributesReservedFlagsAreSet() { }

            public static FileAttributesReservedFlagsAreSet Instance = new FileAttributesReservedFlagsAreSet();
            public static IAction<ShellLinkHeader>[] FixList = { new SetValueFix() };

            public override IAction<ShellLinkHeader>[] Fixes => FixList;

            public override Exception Exception => new ArgumentOutOfRangeException(
                $"{nameof(ShellLinkHeader.FileAttributes)} must have the reserved flags set to zero",
                nameof(ShellLinkHeader.FileAttributes));

            public override bool Check(ShellLinkHeader header) => header.FileAttributes.HasFlag(FileAttributes.Reserved1 | FileAttributes.Reserved2);

            public class SetValueFix : IAction<ShellLinkHeader>
            {
                public void Invoke(ShellLinkHeader value)
                    => value.FileAttributes = value.FileAttributes & ~(FileAttributes.Reserved1 | FileAttributes.Reserved2);
            }
        }

        public sealed class FileAttributesZeroFlags : Error<ShellLinkHeader>
        {
            private FileAttributesZeroFlags() { }

            public static FileAttributesZeroFlags Instance = new FileAttributesZeroFlags();
            public static IAction<ShellLinkHeader>[] FixList = { new SetValueFix() };

            public override IAction<ShellLinkHeader>[] Fixes => FixList;

            public override Exception Exception => new ArgumentOutOfRangeException(
                $"{nameof(ShellLinkHeader.FileAttributes)} must have the final flags set to zero",
                nameof(ShellLinkHeader.FileAttributes));

            public override bool Check(ShellLinkHeader header) => header.FileAttributes.HasFlag(~(FileAttributes.Zero1 - 1));

            public class SetValueFix : IAction<ShellLinkHeader>
            {
                public void Invoke(ShellLinkHeader value)
                    => value.FileAttributes = value.FileAttributes = value.FileAttributes & (FileAttributes.Zero1 - 1);
            }
        }

        public sealed class FileAttributesNormalFlag : Error<ShellLinkHeader>
        {
            private FileAttributesNormalFlag() { }

            public static FileAttributesNormalFlag Instance = new FileAttributesNormalFlag();
            public static IAction<ShellLinkHeader>[] FixList = { new UnsetNormalFix(), new SetNormalFix(), };

            public override IAction<ShellLinkHeader>[] Fixes => FixList;

            public override Exception Exception => new ArgumentOutOfRangeException(
                $"If {nameof(ShellLinkHeader.FileAttributes)} has the flag FILE_ATTRIBUTE_NORMAL, then all other flags must be zero",
                nameof(ShellLinkHeader.FileAttributes));

            public override bool Check(ShellLinkHeader header)
                => header.FileAttributes.HasFlag(FileAttributes.FILE_ATTRIBUTE_NORMAL)
                   && header.FileAttributes != FileAttributes.FILE_ATTRIBUTE_NORMAL;

            public class SetNormalFix : IAction<ShellLinkHeader>
            {
                public void Invoke(ShellLinkHeader value)
                    => value.FileAttributes = value.FileAttributes = FileAttributes.FILE_ATTRIBUTE_NORMAL;
            }

            public class UnsetNormalFix : IAction<ShellLinkHeader>
            {
                public void Invoke(ShellLinkHeader value)
                    => value.FileAttributes = value.FileAttributes = value.FileAttributes & ~FileAttributes.FILE_ATTRIBUTE_NORMAL;
            }
        }

        public sealed class WrongLinkFlags : Error<ShellLinkHeader>
        {
            private WrongLinkFlags() { }

            public static WrongLinkFlags Instance = new WrongLinkFlags();
            public static IAction<ShellLinkHeader>[] FixList = { new SetValueFix() };

            public override IAction<ShellLinkHeader>[] Fixes => FixList;

            public override Exception Exception => new ArgumentOutOfRangeException(
                $"{nameof(ShellLinkHeader.LinkFlags)} must have the final flags set to zero",
                nameof(ShellLinkHeader.LinkFlags));

            public override bool Check(ShellLinkHeader header) => header.LinkFlags.HasFlag(~(LinkFlags.Zero1 - 1));

            public class SetValueFix : IAction<ShellLinkHeader>
            {
                public void Invoke(ShellLinkHeader value) => value.LinkFlags = value.LinkFlags & (LinkFlags.Zero1 - 1);
            }
        }

        public sealed class WrongLinkCLSID : Error<ShellLinkHeader>
        {
            private WrongLinkCLSID() { }

            public static WrongLinkCLSID Instance = new WrongLinkCLSID();
            public static IAction<ShellLinkHeader>[] FixList = { new SetValueFix() };

            public override IAction<ShellLinkHeader>[] Fixes => FixList;

            public override Exception Exception => new ArgumentOutOfRangeException(
                $"{nameof(ShellLinkHeader.LinkCLSID)} must be {ShellLinkHeader.ShellLinkCLSID:D}",
                nameof(ShellLinkHeader.LinkCLSID));

            public override bool Check(ShellLinkHeader header) => header.LinkCLSID != ShellLinkHeader.ShellLinkCLSID;

            public class SetValueFix : IAction<ShellLinkHeader>
            {
                public void Invoke(ShellLinkHeader value) => value.LinkCLSID = ShellLinkHeader.ShellLinkCLSID;
            }
        }

        public sealed class WrongHeaderSize : Error<ShellLinkHeader>
        {
            private WrongHeaderSize() { }

            public static WrongHeaderSize Instance = new WrongHeaderSize();
            public static IAction<ShellLinkHeader>[] FixList = { new SetValueFix() };

            public override IAction<ShellLinkHeader>[] Fixes => FixList;

            public override Exception Exception => new ArgumentOutOfRangeException(
                $"{nameof(ShellLinkHeader.HeaderSize)} must be 0x0000004C",
                nameof(ShellLinkHeader.HeaderSize));

            public override bool Check(ShellLinkHeader header) => header.HeaderSize != 0x0000004C;

            public class SetValueFix : IAction<ShellLinkHeader>
            {
                public void Invoke(ShellLinkHeader value) => value.HeaderSize = 0x0000004C;
            }
        }
    }
}