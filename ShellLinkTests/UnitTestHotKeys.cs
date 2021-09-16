using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShellLink.DataObjects.Enums;

namespace ShellLinkTests
{
    [TestClass]
    public class UnitTestHotKeys
    {
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
