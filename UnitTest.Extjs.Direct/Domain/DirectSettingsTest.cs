using System;
using Extjs.Direct.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Extjs.Direct.Domain
{
    [TestClass]
    public class DirectSettingsTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateNullNamespaceTest()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new DirectSettings(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateEmptyNamespaceTest()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new DirectSettings("");
        }
    }
}
