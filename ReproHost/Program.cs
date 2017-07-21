using System;
using System.Collections.Generic;
using NServiceBus;

namespace ReproHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            HeaderSerializer.CallSerializer();


            Console.ReadKey();
        }
    }
}
