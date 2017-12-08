using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShellLink.DataObjects;
using ShellLink.Internals;
using ShellLink.Loaders;

namespace ShellLinkTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var fname = @"C:\Users\Miguel Angelo\Desktop\Shortcuts\Miguel - Chrome.lnk";
            using (var file = File.Open(fname, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new BinaryReader(file))
            {
                var obj = new ShellLinkObject();
                obj.Load(reader);
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            var fname = @"C:\Users\Default\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\System Tools\Control Panel.lnk";
            using (var file = File.Open(fname, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new BinaryReader(file))
            {
                var obj = new ShellLinkObject();
                obj.Load(reader);
            }
        }

        [TestMethod]
        public void TestReadNullTerminatedString()
        {
            byte[] bytes1 = { 0x20, 0x23, 0xe2, 0x98, 0xa3, 0 };
            string str;

            using (var s = new MemoryStream(bytes1))
            using (var r = new BinaryReader(s))
                str = r.ReadNullTerminatedString(Encoding.UTF8);

            Assert.AreEqual(str, " #☣");
        }

        [TestMethod]
        public void TestHotKeyFlags()
        {
            var strs = AllFlags().Select(f => f.ToString()).ToArray();

            IEnumerable<HotKeyFlags> AllFlags()
            {
                HotKeyFlags[] fs = new[]
                {
                    HotKeyFlags.None,

                    HotKeyFlags.HOTKEYF_ALT,
                    HotKeyFlags.HOTKEYF_CONTROL,
                    HotKeyFlags.HOTKEYF_SHIFT,

                    HotKeyFlags.HOTKEYF_ALT|HotKeyFlags.HOTKEYF_CONTROL,
                    HotKeyFlags.HOTKEYF_ALT|HotKeyFlags.HOTKEYF_SHIFT,

                    HotKeyFlags.HOTKEYF_CONTROL|HotKeyFlags.HOTKEYF_SHIFT,

                    HotKeyFlags.HOTKEYF_ALT|HotKeyFlags.HOTKEYF_CONTROL|HotKeyFlags.HOTKEYF_SHIFT,
                };

                for (int it = 0; it < fs.Length; it++)
                {
                    for (int itN = 0; itN < 10; itN++)
                        yield return fs[it] | (HotKeyFlags.Key_0 + (short)itN);
                    for (short itK = (short)HotKeyFlags.Key_A; itK <= (short)HotKeyFlags.Key_Z; itK++)
                        yield return fs[it] | (HotKeyFlags)itK;
                    for (short itK = (short)HotKeyFlags.VK_F1; itK <= (short)HotKeyFlags.VK_F24; itK++)
                        yield return fs[it] | (HotKeyFlags)itK;
                    yield return fs[it] | HotKeyFlags.VK_NUMLOCK;
                    yield return fs[it] | HotKeyFlags.VK_SCROLL;
                }
            }
        }
    }
}
