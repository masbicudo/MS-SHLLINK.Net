using System;
using System.Collections.Generic;
using ShellLink.DataObjects;

namespace ShellLink.Validators
{
    public static class CommonNetworkRelativeLinkValidator
    {
        public static void Check(this CommonNetworkRelativeLink obj, List<Exception> errors)
        {
        }

        public static bool Repair(this CommonNetworkRelativeLink obj)
        {
            var ok = true;

            return ok;
        }
    }
}