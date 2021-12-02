using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HackTheFuture.A1
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.WaitAny(Algorithm.GetNumbers());
            Task.WaitAny(Algorithm.sendToPuzzleAsync());
        }

    }
}
