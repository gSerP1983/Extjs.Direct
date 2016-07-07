namespace Test.Server.Base
{
    public class BlContextCtorClass
    {
        private readonly object _context;

        public BlContextCtorClass(object context)
        {
            _context = context;
        }

        public string GetContextType()
        {
            return _context.GetType().FullName;
        }
    }
}