using System.Collections.Generic;

namespace Test.Server.Base
{
    public class BlDefaultCtorClass
    {
        public static int Sum(int a, int b)
        {
            return a + b;
        }

        public IEnumerable<Person> GetData()
        {
            yield return new Person { Name = "Jean Luc", Email = "jeanluc.picard@enterprise.com", Phone = "555-111-1111"};
            yield return new Person {Name = "Worf", Email = "worf.moghsson@enterprise.com", Phone = "555-222-2222"};
            yield return new Person {Name = "Deanna", Email = "deanna.troi@enterprise.com", Phone = "555-333-3333"};
            yield return new Person {Name = "Data", Email = "mr.data@enterprise.com", Phone = "555-444-4444"};
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}