using System.IO;
using System.Text;
using JetBrains.Annotations;
using ShellLink.DataObjects;
using ShellLink.ExtraData;
using ShellLink.Internals;

namespace ShellLink.Loaders
{
    public static class ShellLinkObjectLoader
    {
        /// <summary>
        /// 
        /// <para>
        /// Note that no exceptions are thrown because of invalid files.
        /// Call the Check method to see if there are issues with the file,
        /// and Repair method to try to make the file valid.
        /// </para>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="reader"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool Load([NotNull] this ShellLinkObject obj, BinaryReader reader, ShellLinkOptions options = null)
        {
            if (options == null)
            {
                options = new ShellLinkOptions
                {
                    ItemIdProvider = new ItemIDProvider(new IItemIdReader[0]),
                };
            }


            // reading ShellLinkHeader

            var ok = obj.ShellLinkHeader.Load(reader);


            // reading LinkTargetIDList

            if (obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasLinkTargetIDList))
                ok &= obj.LinkTargetIDList.Load(reader, options.ItemIdProvider);


            // reading LinkInfo

            if (obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasLinkInfo))
                ok &= obj.LinkInfo.Load(reader);


            // reading StringData

            var hasNameFlag = obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasName);
            var hasRelativePathFlag = obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasRelativePath);
            var hasWorkingDirFlag = obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasWorkingDir);
            var hasArgumentsFlag = obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasArguments);
            var hasIconLocationFlag = obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasIconLocation);

            var isUnicodeFlag = obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.IsUnicode);
            var szmul = isUnicodeFlag ? 2 : 1;
            var enc = isUnicodeFlag ? Encoding.Unicode : Encoding.Default;

            if (hasNameFlag)
            {
                var sz = reader.ReadUInt16() * szmul;
                obj.StringData.NameString = reader.ReadFixedSizeString(sz, enc);
            }
            if (hasRelativePathFlag)
            {
                var sz = reader.ReadUInt16() * szmul;
                obj.StringData.RelativePath = reader.ReadFixedSizeString(sz, enc);
            }
            if (hasWorkingDirFlag)
            {
                var sz = reader.ReadUInt16() * szmul;
                obj.StringData.WorkingDir = reader.ReadFixedSizeString(sz, enc);
            }
            if (hasArgumentsFlag)
            {
                var sz = reader.ReadUInt16() * szmul;
                obj.StringData.CommandLineArguments = reader.ReadFixedSizeString(sz, enc);
            }
            if (hasIconLocationFlag)
            {
                var sz = reader.ReadUInt16() * szmul;
                obj.StringData.IconLocation = reader.ReadFixedSizeString(sz, enc);
            }

            return ok;
        }
    }
}