using Extjs.Direct.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Extjs.Direct.Domain
{
    [TestClass]
    public class MessagesTest
    {
        [TestMethod]
        public void ResponseMapTest()
        {
            var request = new Request {action = "className", method = "methodName", type = "rpc", tid = 1};
            var response = Response.Map(request);

            Assert.AreEqual(request.action, response.action);
            Assert.AreEqual(request.method, response.method);
            Assert.AreEqual(request.type, response.type);
            Assert.AreEqual(request.tid, response.tid);
        }

        [TestMethod]
        public void ResponseMapNullParameterTest()
        {
            var response = Response.Map(null);
            Assert.IsNull(response);
        }
    }
}
