using Library.IServices;
using System;

namespace Library.Services
{
    internal class TestService : ITestService
    {

        public TestService()
        {
            
        }

        public void Test()
        {
            Console.WriteLine("Echo Success");
        }
    }
}
