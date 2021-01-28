using System;

namespace PPB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting server on port 10001!");
            Http.Server server = new Http.Server(10001);
            server.Start();
        }
    }
}
