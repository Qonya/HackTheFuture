using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HackTheFuture.A3
{
    public static class Algorithm
    {

        public static HttpClient InitClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://involved-htf-challenge.azurewebsites.net");
            var token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiNDIiLCJuYmYiOjE2Mzg0MzU2NzgsImV4cCI6MTYzODUyMjA3OCwiaWF0IjoxNjM4NDM1Njc4fQ.wEA5M_5EtMH3-0VTIoA4JNZjHQ0r4RIYMKyjCq9g0P0gjaufdxN6qCedhnbONATkyKCcBE_82TXUMC5qoHyuTQ";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }
        public async static Task<List<int>> GetDataAsync()
        {
            var client = InitClient();

            var startUrl = "api/path/1/hard/Start";
            var startResponse = await client.GetAsync(startUrl);
            var sampleUrl = "api/path/1/hard/Sample";
            var sampleGetResponse = await client.GetFromJsonAsync<Data>(sampleUrl);

            var sampleAnswer = GetAnswer(sampleGetResponse);
            foreach (var item in sampleAnswer)
            {
                Console.WriteLine(item);
            }
            var samplePostResponse = await client.PostAsJsonAsync<List<int>>(sampleUrl, sampleAnswer);

            var samplePostResponseValue = await samplePostResponse.Content.ReadAsStringAsync();
            Console.WriteLine(samplePostResponseValue);
            return sampleAnswer;

        }
        public static int GetAnswerFunction(Data data, int currentId, int targetId)
        {

            var firstTile = data.Tiles[0];
            var lastTile = data.Tiles[data.Tiles.Count - 1];
            var currentTile = data.Tiles.Find(x => x.Id == currentId);
            var targetTile = data.Tiles.Find(x => x.Id == targetId);
            if (currentTile.Direction == 0)
            {
                if (targetTile.X == currentTile.X && targetTile.Y < currentTile.Y && targetTile.Y >= 1)
                {
                    return targetId;
                }
                else
                {
                    Console.WriteLine("Move not possible");
                }
            }
            if (currentTile.Direction == 1)
            {
                if (targetTile.X - currentTile.X == currentTile.Y - targetTile.Y && targetTile.Y >= 1 && targetTile.X <= lastTile.X)
                {
                    return targetId;
                }
                else
                {
                    Console.WriteLine("Move not possible");
                }
            }
            if (currentTile.Direction == 2)
            {
                if (targetTile.X > currentTile.X && targetTile.Y == currentTile.Y && targetTile.X <= lastTile.X)
                {
                    return targetId;
                }
                else
                {
                    Console.WriteLine("Move not possible");
                }
            }
            if (currentTile.Direction == 3)
            {
                if (targetTile.X - currentTile.X == targetTile.Y - currentTile.Y && targetTile.Y <= lastTile.Y && targetTile.X <= lastTile.X)
                {
                    return targetId;
                }
                else
                {
                    Console.WriteLine("Move not possible");
                }
            }
            if (currentTile.Direction == 4)
            {
                if (targetTile.X == currentTile.X && targetTile.Y > currentTile.Y && targetTile.Y <= lastTile.Y)
                {
                    return targetId;
                }
                else
                {
                    Console.WriteLine("Move not possible");
                }
            }
            if (currentTile.Direction == 5)
            {
                if (currentTile.X - targetTile.X == targetTile.Y - currentTile.Y && targetTile.Y <= lastTile.Y && targetTile.X >= 1)
                {
                    return targetId;
                }
                else
                {
                    Console.WriteLine("Move not possible");
                }
            }
            if (currentTile.Direction == 6)
            {
                if (targetTile.X < currentTile.X && targetTile.Y == currentTile.Y && targetTile.X >= 1)
                {
                    return targetId;
                }
                else
                {
                    Console.WriteLine("Move not possible");
                }
            }
            if (currentTile.Direction == 7)
            {
                if (currentTile.X - targetTile.X == currentTile.Y - targetTile.Y && targetTile.Y >= 1 && targetTile.X >= 1)
                {
                    return targetId;
                }
                else
                {
                    Console.WriteLine("Move not possible");
                }
            }

            return 0;
        }

        public static List<int> GetAnswer(Data data)
        {
            foreach (var item in data.Directions)
            {
                Console.Write(item + "\n");
            }

            var counter = 0;
            foreach (var item in data.Tiles)
            {
                if (counter % data.Tiles[data.Tiles.Count - 1].X == 0)
                {
                    Console.WriteLine();
                }
                Console.Write(data.Directions[item.Direction] + "\t");
                counter++;
            }

            var selectedIds = new List<int>();
            var currentId = 1;
            selectedIds.Add(1);
            do
            {
                Console.WriteLine("Geef uw target Id: ");
                int targetId =  int.Parse(Console.ReadLine());
                var id = GetAnswerFunction(data, currentId, targetId);

            if(id != 0)
            {
                selectedIds.Add(id);
                currentId = targetId;
            }
            } while (currentId != data.Tiles[data.Tiles.Count - 1].Id);
            selectedIds.Add(data.Tiles[data.Tiles.Count - 1].Id);
            return selectedIds;
        }

        public async static Task sendToPuzzleAsync()
        {
            var client = InitClient();

            var puzzleUrl = "api/path/1/hard/Puzzle";
            var puzzleGetResponse = await client.GetFromJsonAsync<Data>(puzzleUrl);

            var puzzleAnswer = GetAnswer(puzzleGetResponse);

            var puzzlePostResponse = await client.PostAsJsonAsync<List<int>>(puzzleUrl, puzzleAnswer);
            var puzzlePostResponseValue = await puzzlePostResponse.Content.ReadAsStringAsync();

            Console.WriteLine(puzzlePostResponseValue);
        }


    }
}
