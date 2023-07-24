using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShellLink.Internals;
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

        [TestMethod]
        public void TestMethod_WhatsappUWP()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "ShellLinkTests.LinkFiles.WhatsApp.lnk";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new BinaryReader(stream))
            {
                var obj = ShellLinkObjectLoader.Load(reader, ShellLinkOptions.AllLoaders());
            }
        }
    }
}
