using System;

namespace UnitTest.Extjs.Direct
{
    public class Rpc
    {
        public static string StaticMethodTwoParameters(string par1, string par2)
        {
            return par1 + "+" + par2 + "=LOVE";
        }

        public string NoStaticMethodTwoParameters(string par1, string par2)
        {
            return par1 + "+" + par2 + "=LOVE";
        }

        public int NoStaticMethodZeroParameter()
        {
            return 0;
        }

        public int ThrowExceptionMethod()
        {
            throw new Exception();
        }
    }
}