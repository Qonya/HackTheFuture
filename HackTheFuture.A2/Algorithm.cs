using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace HackTheFuture.A2
{
    class Algorithm
    {

        static int counter = 0;
        public static HttpClient InitClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://involved-htf-challenge.azurewebsites.net");
            var token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiNDIiLCJuYmYiOjE2Mzg0MzU2NzgsImV4cCI6MTYzODUyMjA3OCwiaWF0IjoxNjM4NDM1Njc4fQ.wEA5M_5EtMH3-0VTIoA4JNZjHQ0r4RIYMKyjCq9g0P0gjaufdxN6qCedhnbONATkyKCcBE_82TXUMC5qoHyuTQ";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        public static int VisitedFloor(int currentFloor, string move)
        {
            counter += 1;
            if (move.Trim().ToLower() == "h")
            {
                currentFloor += counter;
            }
            if (move.Trim().ToLower() == "l")
            {
                currentFloor -= counter;
            }
            return currentFloor;
        }
        public static List<int> GetAnswer(Data data)
        {
            var visitedFloor = 0;
            var visitedFloors = new List<int>();
            visitedFloors.Add(data.Start);
            do
            {
                Console.WriteLine("Current floor: " + data.Start);
                Console.Write("omhoog (H) of omlaag (L): ");
                string move = Console.ReadLine();
                visitedFloor = VisitedFloor(data.Start, move);
                visitedFloors.Add(visitedFloor);
                data.Start = visitedFloor;
            } while (visitedFloor != data.Destination);
            return visitedFloors;
        }

        public async static Task sendToPuzzleAsync()
        {
            counter = 0;
            var client = InitClient();

            var puzzleUrl = "api/path/1/medium/Puzzle";
            var puzzleGetResponse = await client.GetFromJsonAsync<Data>(puzzleUrl);
            
            Console.WriteLine("Start: " + puzzleGetResponse.Start);
            Console.WriteLine("Destination: " + puzzleGetResponse.Destination);
            var puzzleAnswer = GetAnswer(puzzleGetResponse);

            var puzzlePostResponse = await client.PostAsJsonAsync<List<int>>(puzzleUrl, puzzleAnswer);
            var puzzlePostResponseValue = await puzzlePostResponse.Content.ReadAsStringAsync();

            Console.WriteLine(puzzlePostResponseValue);
        }
    }
}
