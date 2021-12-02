using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http;

namespace HackTheFuture.A1
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

        public async static Task<int> GetNumbers()
        {
            var client = InitClient();

            var startUrl = "api/path/1/easy/Start";
            var startResponse = await client.GetAsync(startUrl);
            var sampleUrl = "api/path/1/easy/Sample";
            var sampleGetResponse = await client.GetFromJsonAsync<List<int>>(sampleUrl);

            var sampleAnswer = GetAnswer(sampleGetResponse);
            Console.WriteLine(sampleAnswer);
            return sampleAnswer;
        }
        public static int GetAnswer(List<int> numbers)
        {
            int sum = 0;
            int output = 0;
            int temp = 0;
            foreach (var item in numbers)
            {
                sum += item;
            }
            temp = sum;
            do
            {
                output = 0;
                for (int i = 0; i < sum.ToString().Length; i++)
                {
                    output += temp % 10;
                    temp /= 10;
                }
                temp = output;
                sum = temp;
            } while (output > 10);
            return output;
        }

        public async static Task sendToPuzzleAsync()
        {
            var client = InitClient();
            var sampleUrl = "api/path/1/easy/Sample";

            // We sturen het antwoord met een POST request
            // Het antwoord dat we moeten versturen is een getal dus gebruiken we int
            // De response die we krijgen zal ons zeggen of ons antwoord juist was
            var sampleAnswer = await GetNumbers();
            var samplePostResponse = await client.PostAsJsonAsync<int>(sampleUrl, sampleAnswer);
            // Om te zien of ons antwoord juist was moeten we de response uitlezen
            // Een 200 status code betekent dus niet dat je antwoord juist was!
            var samplePostResponseValue = await samplePostResponse.Content.ReadAsStringAsync();

            // De url om de puzzle challenge data op te halen
            var puzzleUrl = "api/path/1/easy/Puzzle";
            // We doen de GET request en wachten op de het antwoord
            // De response die we verwachten is een lijst van getallen dus gebruiken we List<int>
            var puzzleGetResponse = await client.GetFromJsonAsync<List<int>>(puzzleUrl);

            // Je zoekt het antwoord
            var puzzleAnswer = GetAnswer(puzzleGetResponse);

            // We sturen het antwoord met een POST request
            // Het antwoord dat we moeten versturen is een getal dus gebruiken we int
            // De response die we krijgen zal ons zeggen of ons antwoord juist was
            var puzzlePostResponse = await client.PostAsJsonAsync<int>(puzzleUrl, puzzleAnswer);
            // Om te zien of ons antwoord juist was moeten we de response uitlezen
            // Een 200 status code betekent dus niet dat je antwoord juist was!
            var puzzlePostResponseValue = await puzzlePostResponse.Content.ReadAsStringAsync();

            Console.WriteLine(puzzlePostResponseValue);
        }

    }
}
