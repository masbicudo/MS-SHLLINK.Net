using ShellLink.DataObjects;
using ShellLink.Internals;
using System;
using System.Collections.Generic;

namespace ShellLink.Validators
{
    public static class IDListValidator
    {
        public static void Check(this IDList obj, List<Exception> errors, IOptions options)
        {
            foreach (var itemId in obj.ItemIDList)
            {
                if (itemId == null)
                    errors.Add(new NullReferenceException("ItemID is null"));

                var actuator = options.GetActuatorForShellItemId(itemId.GetType());
                if (!actuator.Check(new(itemId, errors, options)))
                    return;
            }

            if (obj.TerminalID != 0)
                errors.Add(new ArgumentOutOfRangeException(nameof(obj.TerminalID), "TerminalID must be zero."));
        }

        public static bool Repair(this IDList obj, IOptions options)
        {
            obj.ItemIDList.RemoveAll(x => x == null);

            foreach (var itemId in obj.ItemIDList)
            {
                var actuator = options.GetActuatorForShellItemId(itemId.GetType());
                if (!actuator.Repair(new(itemId, options)))
                    return false;
            }

            obj.TerminalID = 0;

            return true;
        }
    }
}