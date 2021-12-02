using System;
using System.Threading.Tasks;

namespace HackTheFuture.A3
{
    class Program
    {
        static void Main(string[] args)
        {
           Task.WaitAny(Algorithm.sendToPuzzleAsync());
        }
    }
}
