using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ShellLink.Validators;

namespace ShellLink.Validators
{
    public static class LinkTargetIDListValidator
    {
        const int TerminalID_Size = ItemID.SizeFieldLength;

        public static void Check(this LinkTargetIDList obj, List<Exception> errors)
        {
            obj.IDList.Check(errors);

            var sum = obj.IDList.ItemIDList
                          .Where(x => x != null)
                          .Sum(x => x.GetDataLength() + ItemId.SizeFieldLength) + TerminalID_Size;

            if (obj.IDListSize != sum)
                errors.Add(new InvalidDataException("IDListSize must match the size of all ItemIDList items combined."));
        }

        public static bool Repair(this LinkTargetIDList obj)
        {
            obj.IDList.Repair();

            var sum = obj.IDList.ItemIDList
                .Where(x => x != null)
                .Sum(x => x.GetDataLength() + TerminalID_Size);

            obj.IDListSize = (ushort)sum;

            return true;
        }
    }
}