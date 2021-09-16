using ShellLink.DataObjects;
using System;
using System.Collections.Generic;

namespace ShellLink.Validators
{
    public static class LinkInfoValidator
    {
        public static void Check(this LinkInfo obj, List<Exception> errors)
        {
        }

        public static bool Repair(this LinkInfo obj)
        {
            var ok = true;

            return ok;
        }
    }
}