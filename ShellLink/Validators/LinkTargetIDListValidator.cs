using ShellLink.DataObjects;
using ShellLink.Internals;
using ShellLink.ItemID;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ShellLink.Validators
{
    public static class LinkTargetIDListValidator
    {
        private const int TerminalID_Size = 2;

        public static void Check(this LinkTargetIDList obj, List<Exception> errors, IOptions options)
        {
            obj.IDList.Check(errors, options);

            

            var sum = obj.IDList.ItemIDList
                        .Where(x => x != null)
                        .Sum(x => options.GetActuatorForShellItemId(x.GetType()).GetLength(new(x, options)))
                        + TerminalID_Size;

            if (obj.IDListSize != sum)
                errors.Add(new InvalidDataException("IDListSize must match the size of all ItemIDList items combined."));
        }

        public static bool Repair(this LinkTargetIDList obj, IOptions options)
        {
            obj.IDList.Repair(options);

            var sum = obj.IDList.ItemIDList
                .Sum(x => options.GetActuatorForShellItemId(x.GetType()).GetLength(new(x, options)))
                + TerminalID_Size;

            obj.IDListSize = (ushort)sum;

            return true;
        }
    }
}