using Extjs.Direct.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Extjs.Direct.Util
{
    [TestClass]
    public class MethodUtilTest
    {
        private ObjectTest _obj;
        [TestInitialize]
        public void Initialize()
        {
            _obj = new ObjectTest();
        }

        [TestMethod]
        public void CallStaticTest()
        {
            var mi = _obj.GetType().GetMethod("GetIntValue");
            var res = MethodUtil.CallStatic(mi);
            Assert.AreEqual((int)res, 5);
        }

        [TestMethod]
        public void CallTest()
        {
            var mi = _obj.GetType().GetMethod("GetValue");
            var res = MethodUtil.Call(_obj, mi, new object[] { true });
            Assert.AreEqual((int)res, 1);
        }

        [TestMethod]
        public void FindTest()
        {
            var mi = _obj.GetType().GetMethod("GetIntValue");

            var res = MethodUtil.Find(_obj, "GetIntValue");
            Assert.AreEqual(mi, res[0]);

            res = MethodUtil.Find(typeof (ObjectTest), "GetIntValue");
            Assert.AreEqual(mi, res[0]);

            res = MethodUtil.Find<ObjectTest>("GetIntValue");
            Assert.AreEqual(mi, res[0]);
        }
    }

    public class ObjectTest
    {
        public static int GetIntValue()
        {
            return 5;
        }

        public int GetValue(bool flag)
        {
            if (flag)
                return 1;
            return 0;
        }
    }
}
