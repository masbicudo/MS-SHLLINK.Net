using JetBrains.Annotations;
using ShellLink.Actuators;
using ShellLink.DataObjects;
using ShellLink.DataObjects.Enums;
using ShellLink.Internals;
using System.IO;
using System.Text;

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
        public static bool Load(
                [NotNull] this ShellLinkObject obj,
                BinaryReader reader,
                ShellLinkOptions options = null
            )
        {
            options = ShellLinkOptions.Normalize(options);


            // reading ShellLinkHeader

            var ok = obj.ShellLinkHeader.Load(reader);


            // reading LinkTargetIDList

            if (obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasLinkTargetIDList))
            {
                obj.LinkTargetIDList = obj.LinkTargetIDList ?? new LinkTargetIDList();
                ok &= obj.LinkTargetIDList.Load(reader, options);
            }
            else
            {
                obj.LinkTargetIDList = null;
            }


            // reading LinkInfo

            if (obj.ShellLinkHeader.LinkFlags.HasFlag(LinkFlags.HasLinkInfo))
            {
                obj.LinkInfo = obj.LinkInfo ?? new LinkInfo();
                ok &= obj.LinkInfo.Load(reader);
            }
            else
            {
                obj.LinkInfo = null;
            }


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
                obj.StringData.NameString = reader.ReadFixedSizeString(sz, enc, ZeroCharBehavior.RemoveTrailing);
            }
            if (hasRelativePathFlag)
            {
                var sz = reader.ReadUInt16() * szmul;
                obj.StringData.RelativePath = reader.ReadFixedSizeString(sz, enc, ZeroCharBehavior.RemoveTrailing);
            }
            if (hasWorkingDirFlag)
            {
                var sz = reader.ReadUInt16() * szmul;
                obj.StringData.WorkingDir = reader.ReadFixedSizeString(sz, enc, ZeroCharBehavior.RemoveTrailing);
            }
            if (hasArgumentsFlag)
            {
                var sz = reader.ReadUInt16() * szmul;
                obj.StringData.CommandLineArguments = reader.ReadFixedSizeString(sz, enc, ZeroCharBehavior.RemoveTrailing);
            }
            if (hasIconLocationFlag)
            {
                var sz = reader.ReadUInt16() * szmul;
                obj.StringData.IconLocation = reader.ReadFixedSizeString(sz, enc, ZeroCharBehavior.RemoveTrailing);
            }


            // reading LinkInfo

            ok &= obj.ExtraData.Load(reader, options);

            ok &= reader.BaseStream.Position == reader.BaseStream.Length;

            return ok;
        }

        public static ShellLinkObject Load(BinaryReader reader)
        {
            var obj = new ShellLinkObject();
            Load(obj, reader);
            return obj;
        }

        public static ShellLinkObject Load(BinaryReader reader, ShellLinkOptions options)
        {
            var obj = new ShellLinkObject();
            Load(obj, reader, options);
            return obj;
        }
    }
}