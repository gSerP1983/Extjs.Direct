using System;
using Nancy.Hosting.Self;

namespace Test.Nancy.SelfHost
{
    class Program
    {
        static void Main()
        {
            const string url = "http://localhost:52517/";
            using (var host = new NancyHost(new Uri(url)))
            {
                host.Start();
                Console.WriteLine(url + " started...");
                Console.ReadLine();
            }
        }
    }
}
