using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackTheFuture.A2
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.WaitAny(Algorithm.sendToPuzzleAsync());
            Console.ReadLine();
        }
    }
}
