using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using d05.Nasa.Apod.Models;

namespace d05.Nasa
{
    public class LastDaysNasaClient : INasaClient<int, Task<MediaOfToday[]>>
    {
        private string _requestUrl = "https://api.nasa.gov/planetary/apod";
        private string _apiKey;
        private string[] _parameters = {"start_date", "api_key"};

        private TimeSpan _timeout = TimeSpan.FromSeconds(20);

        public LastDaysNasaClient(string apiKey)
        {
            _apiKey = new string(apiKey);
        }
        
        public async Task<MediaOfToday[]> GetAsync(int N)
        {
            var medias = new List<MediaOfToday>();
            
            var client = new HttpClient();
            client.Timeout = _timeout;
            DateTime date = DateTime.Now.AddDays(-N + 1);
            _requestUrl += 
                $"?{_parameters[0]}={date.ToString("yyyy-MM-dd", new CultureInfo("en-US"))}" +
                $"&{_parameters[1]}={_apiKey}";

            try
            {
                var response = await client.GetAsync(_requestUrl);
                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"GET\n\"{_requestUrl}\" returned {response.StatusCode}:\n" +
                                      $"{error}");
                    return medias.ToArray();
                }
                string json = await response.Content.ReadAsStringAsync();
                string resJson = json.Insert(0, "{ \"results\" :") + "}";
                JsonElement jsonArray = JsonDocument.Parse(resJson)
                    .RootElement.GetProperty("results");
                foreach (var element in jsonArray.EnumerateArray())
                    medias.Add(JsonSerializer.Deserialize<MediaOfToday>(element.ToString() ?? string.Empty));
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Request has reached time out");
                return medias.ToArray();
            }
            catch
            {
                Console.WriteLine("Something went wrong");
                return medias.ToArray();
            }

            return medias.ToArray();
        }
    }
}