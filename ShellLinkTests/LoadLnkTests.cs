using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShellLink.Loaders;

namespace ShellLinkTests
{
    [TestClass]
    public class LoadLnkTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var fname = @"C:\Shortcuts\Miguel - Chrome.lnk";
            using (var file = File.Open(fname, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new BinaryReader(file))
            {
                var obj = ShellLinkObjectLoader.Load(reader);
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            var fname = @"C:\Users\Default\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\System Tools\Control Panel.lnk";
            using (var file = File.Open(fname, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new BinaryReader(file))
            {
                var obj = ShellLinkObjectLoader.Load(reader);
            }
        }
    }
}
