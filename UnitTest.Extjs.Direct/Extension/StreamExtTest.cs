using System.IO;
using Extjs.Direct.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Extjs.Direct.Extension
{
    [TestClass]
    public class StreamExtTest
    {
        [TestMethod]
        public void NullStreamTest()
        {
            Assert.AreEqual("", (null as Stream).AsString());
        }

        [TestMethod]
        public void StreamAsStringTest()
        {
            const string text = "some text";

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(text);
                    writer.Flush();

                    Assert.AreEqual(stream.AsString(), text);
                }
            }
        }
    }
}
