using System;
using Extjs.Direct.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Extjs.Direct.Extension
{
    [TestClass]
    public class TypeExtTest
    {
        [TestMethod]
        public void IsSimpleTypeTest()
        {
            Assert.IsTrue(typeof(string).IsSimpleType());
            Assert.IsFalse(typeof(AClass).IsSimpleType());
            Assert.IsTrue(typeof(decimal).IsSimpleType());
            Assert.IsFalse(typeof(CClass<>).IsSimpleType());
            Assert.IsTrue(typeof(Guid).IsSimpleType());
        }

        [TestMethod]
        public void GetUnderlyingTypeTest()
        {
            Assert.AreEqual(typeof(AClass).GetNullableUnderlyingType(), typeof(AClass));
            Assert.AreEqual(typeof(IInterface<>).GetNullableUnderlyingType(), typeof(IInterface<>));
            Assert.AreNotEqual(typeof(int?).GetNullableUnderlyingType(), typeof(int?));
            Assert.AreEqual(typeof(int?).GetNullableUnderlyingType(), typeof(int));
        }


        #region helpers

        public interface IInterface
        {
            void Test1() ;
        }

        // ReSharper disable once UnusedTypeParameter
        public interface IInterface<T>: IInterface
        {
        }

        public class AClass : IInterface
        {
            public void Test1() { }
            public void Test2() { }
        }

        public class BClass : AClass
        {
            public void Test3() { }
        }

        public class CClass<T> : IInterface<T>
        {
            public void Test1() { }
        }

        #endregion

    }
}
