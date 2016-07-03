using Extjs.Direct.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Extjs.Direct.Util
{
    [TestClass]
    public class TypeResolverTest
    {
        [TestMethod]
        public void GetTestMethod()
        {
            TypeResolver.Initialize(GetType().Assembly, typeof(int).Assembly);
            Assert.AreEqual(typeof(TypeResolverTest), TypeResolver.Get(GetType().FullName));
            Assert.IsNull(TypeResolver.Get(" "));
        }
    }
}
