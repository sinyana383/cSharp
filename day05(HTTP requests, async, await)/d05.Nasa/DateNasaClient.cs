using System;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using d05.Nasa.Apod.Models;

namespace d05.Nasa
{
    public class DateNasaClient : INasaClient<DateTime, Task<MediaOfToday>>
    {
        private string _requestUrl = "https://api.nasa.gov/planetary/apod";
        private string _apiKey;
        private string[] _parameters = {"date", "api_key"};

        private TimeSpan _timeout = TimeSpan.FromSeconds(20);

        public DateNasaClient(string apiKey)
        {
            _apiKey = new string(apiKey);
        }
        
        public async Task<MediaOfToday> GetAsync(DateTime date)
        {
            var media = new MediaOfToday();
            
            var client = new HttpClient();
            client.Timeout = _timeout;
            _requestUrl += 
                $"?{_parameters[0]}={date.ToString("yyyy-mm-dd", new CultureInfo("en-US"))}" +
                $"&{_parameters[1]}={_apiKey}";

            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync(_requestUrl);
                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"GET\n\"{_requestUrl}\" returned {response.StatusCode}:\n" +
                                      $"{error}");
                    return media;
                }
                string json = await response.Content.ReadAsStringAsync();
                string resJson = json.Insert(0, "{ \"results\" :") + "}";
                JsonElement jsonElement = JsonDocument.Parse(resJson)
                    .RootElement.GetProperty("results");
                media = JsonSerializer.Deserialize<MediaOfToday>(jsonElement.ToString() ?? string.Empty);
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Request has reached time out");
                return media;
            }
            catch
            {
                Console.WriteLine("Something went wrong");
                return media;
            }

            return media;
        }
    }
}