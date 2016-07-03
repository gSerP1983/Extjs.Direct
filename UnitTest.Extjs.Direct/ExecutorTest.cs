using System;
using System.Collections.Generic;
using System.Linq;
using Extjs.Direct;
using Extjs.Direct.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace UnitTest.Extjs.Direct
{
    [TestClass]
    public class TypeDirectExecutorTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Executor.Initialize(GetRpcTypes(), CreateObject, new DirectSettings("Namespace"), LogError);
        }

        private static void LogError(Exception x)
        {
            Assert.IsNotNull(x);
        }

        private static object CreateObject(Type type, object context)
        {
            return Activator.CreateInstance(type);
        }

        private static IEnumerable<Type> GetRpcTypes()
        {
            yield return typeof(Rpc);
        }

        [TestMethod]
        public void MetaTest()
        {
            var meta = Executor.Instance.Meta();
            Assert.IsNotNull(meta);

            var methods = typeof(Rpc).GetMethods()
                .Where(x => x.GetBaseDefinition().DeclaringType != typeof(object))
                .ToArray();

            Assert.IsTrue(methods.Count() == meta.actions[meta.actions.Keys.Single()].Length);
            Assert.IsTrue(meta.actions[meta.actions.Keys.Single()].ToList().All(x => methods.Any(y => y.Name == x.name)));
        }

        [TestMethod]
        public void MetaScriptTest()
        {
            var script = Executor.Instance.MetaScript();
            Assert.IsNotNull(script);
            Assert.IsTrue(script.Contains("Ext.app.REMOTING_API"));
        }

        [TestMethod]
        public void ExecuteTest()
        {
            var request = new Request
            {
                action = typeof(Rpc).FullName,
                method = "NoStaticMethodTwoParameters",
                type = "rpc",
                tid = 1,
                data = new object[] { "1", "2" }
            };
            var result = Executor.Instance.Execute(JsonConvert.SerializeObject(request), null) as Response[];
            Assert.IsNotNull(result);

            var response = result.First();
            Assert.IsNotNull(response);
            Assert.IsTrue(response.meta.success);
            Assert.AreEqual("1+2=LOVE", response.result);
        }

        [TestMethod]
        public void EmptyRequestStringExecuteTest()
        {
            var result = Executor.Instance.Execute(null, null) as ResponseMeta;
            Assert.IsNotNull(result);
            Assert.IsFalse(result.success);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.msg));
        }

        [TestMethod]
        public void InvalidRequestStringExecuteTest()
        {
            var result = Executor.Instance.Execute("wrong request", null) as ResponseMeta;
            Assert.IsNotNull(result);
            Assert.IsFalse(result.success);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.msg));
        }

        [TestMethod]
        public void MissingMethodExecuteTest()
        {
            var request = new Request { action = typeof(Rpc).FullName, method = "Method", type = "rpc", tid = 1, data = null};
            var result = Executor.Instance.Execute(JsonConvert.SerializeObject(request), null) as Response[];
            Assert.IsNotNull(result);

            var response = result.First();
            Assert.IsNotNull(response);
            Assert.IsFalse(response.meta.success);
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.meta.msg));
        }

        [TestMethod]
        public void EmptyTypeExecuteTest()
        {
            var request = new Request { action = "", method = "Method", type = "rpc", tid = 1, data = null };
            var result = Executor.Instance.Execute(JsonConvert.SerializeObject(request), null) as Response[];
            Assert.IsNotNull(result);

            var response = result.First();
            Assert.IsNotNull(response);
            Assert.IsFalse(response.meta.success);
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.meta.msg));
        }

        [TestMethod]
        public void ErrorParametersMethodExecuteTest()
        {
            var request = new Request
            {
                action = typeof(Rpc).FullName,
                method = "NoStaticMethodTwoParameters", 
                type = "rpc", 
                tid = 1,
                data = new object[]{"1","2", "3"}
            };
            var result = Executor.Instance.Execute(JsonConvert.SerializeObject(request), null) as Response[];
            Assert.IsNotNull(result);

            var response = result.First();
            Assert.IsNotNull(response);
            Assert.IsFalse(response.meta.success);
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.meta.msg));
        }

        [TestMethod]
        public void MissingTypeExecuteTest()
        {
            var request = new Request { action = "MissingType", method = "Method", type = "rpc", tid = 1 };
            var result = Executor.Instance.Execute(JsonConvert.SerializeObject(request), null) as Response[];
            Assert.IsNotNull(result);

            var response = result.First();
            Assert.IsNotNull(response);
            Assert.IsFalse(response.meta.success);
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.meta.msg));
        }
    }
}
