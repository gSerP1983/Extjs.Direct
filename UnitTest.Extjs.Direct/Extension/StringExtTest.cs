using Extjs.Direct.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Extjs.Direct.Extension
{
    [TestClass]
    public class StringExtTest
    {
        [TestMethod]
        public void IsJsonArrayTest()
        {
            Assert.IsTrue("[{id:1},{id:2}]".IsJsonArray());
            Assert.IsFalse("{id:1}".IsJsonArray());
            Assert.IsFalse("simple string".IsJsonArray());
        }

        [TestMethod]
        public void IsIsJsonObjectTest()
        {
            Assert.IsFalse("[{id:1},{id:2}]".IsJsonObject());
            Assert.IsTrue("{id:1}".IsJsonObject());
            Assert.IsFalse("simple string".IsJsonObject());
        }

        [TestMethod]
        public void IsIsJsonTest()
        {
            Assert.IsTrue("[{id:1},{id:2}]".IsJson());
            Assert.IsTrue("{id:1}".IsJson());
            Assert.IsFalse("simple string".IsJson());
        }
    }
}
