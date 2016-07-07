using System;
using Extjs.Direct.Extension;
using Extjs.Direct.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Extjs.Direct.Util
{
    [TestClass]
    public class MethodCallerTest
    {
        [TestMethod]
        public void MethodEmptyParametersCallTest()
        {
            Test("Method1", new object[] { }, 1);
        }

        [TestMethod]
        public void MethodSimpleParameterCallTest()
        {
            Test("Method2", new object[] { false }, 0);
            Test("Method2", new object[] { (bool?)false }, 0);
        }

        [TestMethod]
        public void MethodGuidParameterCallTest()
        {
            var guid = Guid.NewGuid();
            Test("MethodGuid", new object[] { guid }, guid.ToString());
            Test("MethodGuid", new object[] { (Guid?)guid }, guid.ToString());
        }

        [TestMethod]
        public void MethodNullableGuidParameterCallTest()
        {
            var guid = Guid.NewGuid();
            Test("MethodNullableGuid", new object[] { guid }, guid.ToString());
            Test("MethodNullableGuid", new object[] { (Guid?)guid }, guid.ToString());
        }

        [TestMethod]
        public void MethodTypeParameterCallTest()
        {
            Test("Method4", new object[] { typeof(ObjectClass2) }, typeof(ObjectClass2).FullName, new object[]{""});
        }

        [TestMethod]
        public void MethodValueObjectParameterCallTest()
        {
            // the object is transferred directly
            Test("Method3", new object[] { new ObjectClass2() }, 111);
        }

        [TestMethod]
        public void MethodValueObjectParameterAsJsonCallTest()
        {
            // the object is transferred as json-string
            Test("Method3", new object[] { new ObjectClass2().AsJson() }, 111);
        }

        [TestMethod]
        public void MethodEmptyParametersWithGenericArgumentsCallTest()
        {
            // the object is transferred as json-string
            Test("Method5", new object[] { }, typeof(ObjectClass2).FullName, new object[]{typeof(ObjectClass2)});
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void MethodEmptyParametersCallWithInvalidParametersCountTest()
        {
            Test("Method1", new object[] { "" }, 111);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void MethodEmptyParametersCallNotFoundTypeInServiceLocatorCountTest()
        {
            Test("Method1", new object[] { 1, 2, "" }, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MethodEmptyParametersWithGenericArgumentCallWithInvalidValueTest1()
        {
            Test("Method5", new object[] { }, null, new object[] { null });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MethodEmptyParametersWithGenericArgumentCallWithInvalidValueTest2()
        {
            Test("Method5", new object[] { }, null, new object[] { "" });
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void MethodEmptyParametersWithGenericArgumentCallWithInvalidValueTest3()
        {
            Test("Method5", new object[] { }, null, new object[] { "111" });
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void MethodNullTypeParameterCallTest()
        {
            Test("Method4", new object[] { null }, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void NotFoundTypeInServiceLocatorTest()
        {
            Test(null, "Method2", new object[] { true }, null, null);
        }

        private void Test(string methodName, object[] parameters, object result, object[] genericArguments = null )
        {
            Test(new ObjectClass1(), methodName, parameters, result, genericArguments);
        }

        private void Test(object obj, string methodName, object[] parameters, object result, object[] genericArguments)
        {
            var methInfo = typeof(ObjectClass1).GetMethod(methodName);
            Assert.IsNotNull(methInfo);
            var res = MethodCaller.GetResult(obj, methInfo, parameters, genericArguments);
            Assert.AreEqual(res, result);
        }
    }

    public class ObjectClass1
    {
        public static int Method1()
        {
            return 1;
        }

        public int Method2(bool flag)
        {
            if (flag)
                return 1;
            return 0;
        }

        public int Method3(ObjectClass2 obj)
        {
            return obj.Value;
        }

        public string Method4(Type type)
        {
            return type.FullName;
        }

        public string Method5<T>()
        {
            return typeof(T).FullName;
        }

        public string MethodGuid(Guid guid)
        {
            return guid.ToString();
        }

        public string MethodNullableGuid(Guid? guid)
        {
            return guid.ToString();
        }

    }

    public class ObjectClass2
    {
        public int Value { get { return 111; } }
    }
}
