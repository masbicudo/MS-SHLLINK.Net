using System;
using System.Collections.Generic;
using ShellLink.DataObjects;

namespace ShellLink.Validators
{
    public static class ShellLinkObjectValidator
    {
        /// <summary>
        /// Checks the object for errors in field values,
        /// and inconsistencies between them.
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="findFiles">Indicates that references to files that don't exist should be considered as errors.</param>
        public static void Check(this ShellLinkObject obj, List<Exception> errors, bool findFiles = false)
        {
            obj.ShellLinkHeader.Check(errors);

            var hasLinkTargetIDListFlag = obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasLinkTargetIDList);
            if (hasLinkTargetIDListFlag && obj.LinkTargetIDList == null)
                errors.Add(new ArgumentNullException($"{nameof(LinkTargetIDList)} should not be null", nameof(LinkTargetIDList)));
            if (!hasLinkTargetIDListFlag && obj.LinkTargetIDList != null)
                errors.Add(new ArgumentException($"{nameof(LinkTargetIDList)} should be null", nameof(LinkTargetIDList)));

            obj.LinkTargetIDList?.Check(errors);


            obj.LinkInfo?.Check(errors);


            var hasNameFlag = obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasName);
            var hasRelativePathFlag = obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasRelativePath);
            var hasWorkingDirFlag = obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasWorkingDir);
            var hasArgumentsFlag = obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasArguments);
            var hasIconLocationFlag = obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasIconLocation);

            if (hasNameFlag && obj.StringData.NameString == null)
                errors.Add(new ArgumentNullException($"{nameof(StringData.NameString)} should not be null", nameof(StringData.NameString)));
            if (!hasNameFlag && obj.LinkTargetIDList != null)
                errors.Add(new ArgumentException($"{nameof(StringData.NameString)} should be null", nameof(StringData.NameString)));

            if (hasRelativePathFlag && obj.StringData.RelativePath == null)
                errors.Add(new ArgumentNullException($"{nameof(StringData.RelativePath)} should not be null", nameof(StringData.RelativePath)));
            if (!hasRelativePathFlag && obj.LinkTargetIDList != null)
                errors.Add(new ArgumentException($"{nameof(StringData.RelativePath)} should be null", nameof(StringData.RelativePath)));

            if (hasWorkingDirFlag && obj.StringData.WorkingDir == null)
                errors.Add(new ArgumentNullException($"{nameof(StringData.WorkingDir)} should not be null", nameof(StringData.WorkingDir)));
            if (!hasWorkingDirFlag && obj.LinkTargetIDList != null)
                errors.Add(new ArgumentException($"{nameof(StringData.WorkingDir)} should be null", nameof(StringData.WorkingDir)));

            if (hasArgumentsFlag && obj.StringData.CommandLineArguments == null)
                errors.Add(new ArgumentNullException($"{nameof(StringData.CommandLineArguments)} should not be null", nameof(StringData.CommandLineArguments)));
            if (!hasArgumentsFlag && obj.LinkTargetIDList != null)
                errors.Add(new ArgumentException($"{nameof(StringData.CommandLineArguments)} should be null", nameof(StringData.CommandLineArguments)));

            if (hasIconLocationFlag && obj.StringData.IconLocation == null)
                errors.Add(new ArgumentNullException($"{nameof(StringData.IconLocation)} should not be null", nameof(StringData.IconLocation)));
            if (!hasIconLocationFlag && obj.LinkTargetIDList != null)
                errors.Add(new ArgumentException($"{nameof(StringData.IconLocation)} should be null", nameof(StringData.IconLocation)));
        }

        /// <summary>
        /// Tryes to repair fields with wrong values,
        /// and inconsistencies when there is only one way to fix the problem.
        /// When multiple alternatives are possible, it cannot be fixed automaticaly.
        /// If everything is ok, the method returns true;
        /// otherwise, if something is still wrong, the return is false.
        /// <para>
        /// This method should be called before saving the object to a file,
        /// otherwise the file will contain invalid data inside of it.
        /// </para>
        /// </summary>
        /// <param name="searchFiles">Indicates that missing files should be replaced by valid files if a similar file can be found.</param>
        /// <returns></returns>
        public static bool Repair(this ShellLinkObject obj, bool searchFiles = false)
        {
            bool ok = true;


            // fixing ShellLinkHeader

            ok &= obj.ShellLinkHeader.Repair();


            // fixing LinkTargetIDList

            var hasLinkTargetIDListFlag = obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasLinkTargetIDList);
            if (hasLinkTargetIDListFlag && obj.LinkTargetIDList == null)
            {
                obj.ShellLinkHeader.LinkFlags |= LinkFlags.HasLinkTargetIDList;
                obj.LinkTargetIDList = new LinkTargetIDList();
            }
            if (!hasLinkTargetIDListFlag && obj.LinkTargetIDList != null)
            {
                obj.ShellLinkHeader.LinkFlags |= LinkFlags.HasLinkTargetIDList;
            }

            ok &= obj.LinkTargetIDList?.Repair() ?? true;


            // fixing LinkInfo

            ok &= obj.LinkInfo?.Repair() ?? true;


            // fixing StringData

            var hasNameFlag = obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasName);
            var hasRelativePathFlag = obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasRelativePath);
            var hasWorkingDirFlag = obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasWorkingDir);
            var hasArgumentsFlag = obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasArguments);
            var hasIconLocationFlag = obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasIconLocation);

            if (hasNameFlag && obj.StringData.NameString == null)
                obj.ShellLinkHeader.LinkFlags &= ~LinkFlags.HasName;
            if (!hasNameFlag && obj.LinkTargetIDList != null)
                obj.ShellLinkHeader.LinkFlags |= LinkFlags.HasName;

            if (hasRelativePathFlag && obj.StringData.RelativePath == null)
                obj.ShellLinkHeader.LinkFlags &= ~LinkFlags.HasRelativePath;
            if (!hasRelativePathFlag && obj.LinkTargetIDList != null)
                obj.ShellLinkHeader.LinkFlags |= LinkFlags.HasRelativePath;

            if (hasWorkingDirFlag && obj.StringData.WorkingDir == null)
                obj.ShellLinkHeader.LinkFlags &= ~LinkFlags.HasWorkingDir;
            if (!hasWorkingDirFlag && obj.LinkTargetIDList != null)
                obj.ShellLinkHeader.LinkFlags |= LinkFlags.HasWorkingDir;

            if (hasArgumentsFlag && obj.StringData.CommandLineArguments == null)
                obj.ShellLinkHeader.LinkFlags &= ~LinkFlags.HasArguments;
            if (!hasArgumentsFlag && obj.LinkTargetIDList != null)
                obj.ShellLinkHeader.LinkFlags |= LinkFlags.HasArguments;

            if (hasIconLocationFlag && obj.StringData.IconLocation == null)
                obj.ShellLinkHeader.LinkFlags &= ~LinkFlags.HasIconLocation;
            if (!hasIconLocationFlag && obj.LinkTargetIDList != null)
                obj.ShellLinkHeader.LinkFlags |= LinkFlags.HasIconLocation;

            return ok;
        }
    }
}