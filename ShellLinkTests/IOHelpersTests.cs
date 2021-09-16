using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShellLink.Internals;

namespace ShellLinkTests
{
    [TestClass]
    public class IOHelpersTests
    {
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
        public void TestWriteNullTerminatedString()
        {
            using (var s = new MemoryStream())
            using (var w = new BinaryWriter(s))
            {
                var count = w.WriteNullTerminatedString("MASBicudo", Encoding.Unicode, 5, 10);
                var bytes = s.ToArray();
                Assert.AreEqual(4, count);
                CollectionAssert.AreEqual(new byte[] { 77, 0, 65, 0, 83, 0, 66, 0, 0, 0 }, bytes);
            }

            using (var s = new MemoryStream())
            using (var w = new BinaryWriter(s))
            {
                var count = w.WriteNullTerminatedString("MASBicudo", Encoding.Unicode, 5, 10, true);
                var bytes = s.ToArray();
                Assert.AreEqual(4, count);
                CollectionAssert.AreEqual(new byte[] { 77, 0, 65, 0, 83, 0, 66, 0, 0, 0 }, bytes);
            }

            using (var s = new MemoryStream())
            using (var w = new BinaryWriter(s))
            {
                var count = w.WriteNullTerminatedString("M", Encoding.Unicode, 5, 10);
                var bytes = s.ToArray();
                Assert.AreEqual(1, count);
                CollectionAssert.AreEqual(new byte[] { 77, 0, 0, 0, 0 }, bytes);
            }

            using (var s = new MemoryStream())
            using (var w = new BinaryWriter(s))
            {
                var count = w.WriteNullTerminatedString("Mig", Encoding.Unicode, 8, 8);
                var bytes = s.ToArray();
                Assert.AreEqual(3, count);
                CollectionAssert.AreEqual(new byte[] { 77, 0, 105, 0, 103, 0, 0, 0 }, bytes);
            }

            using (var s = new MemoryStream())
            using (var w = new BinaryWriter(s))
            {
                var count = w.WriteNullTerminatedString("MASBicudo", Encoding.Unicode, 9, 9);
                var bytes = s.ToArray();
                Assert.AreEqual(3, count);
                CollectionAssert.AreEqual(new byte[] { 77, 0, 65, 0, 83, 0, 0, 0, 0 }, bytes);
            }

            using (var s = new MemoryStream())
            using (var w = new BinaryWriter(s))
            {
                var count = w.WriteNullTerminatedString("MASBicudo", Encoding.Unicode, 0, 20);
                var bytes = s.ToArray();
                Assert.AreEqual(9, count);
                CollectionAssert.AreEqual(new byte[] { 77, 0, 65, 0, 83, 0, 66, 0, 105, 0, 99, 0, 117, 0, 100, 0, 111, 0, 0, 0 }, bytes);
            }

            using (var s = new MemoryStream())
            using (var w = new BinaryWriter(s))
            {
                var count = w.WriteNullTerminatedString("MASBicudo", Encoding.Unicode, 0, 32);
                var bytes = s.ToArray();
                Assert.AreEqual(9, count);
                CollectionAssert.AreEqual(new byte[] { 77, 0, 65, 0, 83, 0, 66, 0, 105, 0, 99, 0, 117, 0, 100, 0, 111, 0, 0, 0 }, bytes);
            }

            using (var s = new MemoryStream())
            using (var w = new BinaryWriter(s))
            {
                var count = w.WriteNullTerminatedString("MASBicudo", Encoding.Unicode, 0, 0);
                var bytes = s.ToArray();
                Assert.AreEqual(0, count);
                CollectionAssert.AreEqual(new byte[] { }, bytes);
            }

            using (var s = new MemoryStream())
            using (var w = new BinaryWriter(s))
            {
                var count = w.WriteNullTerminatedString("MASBicudo", Encoding.Unicode, 0, 1);
                var bytes = s.ToArray();
                Assert.AreEqual(0, count);
                CollectionAssert.AreEqual(new byte[] { 0 }, bytes);
            }
        }
    }
}
