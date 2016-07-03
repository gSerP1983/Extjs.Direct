using System;
using Extjs.Direct.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Extjs.Direct.Domain
{
    [TestClass]
    public class MetaTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullMethodInfoDirectMethodCreateTest()
        {
            DirectMethod.Create(null);
        }
    }
}
