using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using d05.Nasa;
using d05.Nasa.Apod;
using d05.Nasa.Apod.Models;
using Microsoft.Extensions.Configuration;

if (args.Length < 2)
    return ErrorMassage("Wrong number of arguments to input");
string baseDirectory = Directory.GetCurrentDirectory();
string fullPath = Path.Combine(baseDirectory, "appsettings.json");
if (!File.Exists(fullPath))
    return ErrorMassage($"{fullPath} not found");

IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddJsonFile(fullPath);
IConfiguration root = configurationBuilder.Build();

List<MediaOfToday> result = new List<MediaOfToday>();
if (args[0] == "apod")
{
    INasaClient<int, Task<MediaOfToday[]>> client;
    client = new ApodClient(root["ApiKey"]);
    if (!int.TryParse(args[1], out int N))
        return ErrorMassage($"{args[1]}: wrong argument format");

    Task<MediaOfToday[]> res = client.GetAsync(N);
    MediaOfToday[] temp = await res;
    result = new List<MediaOfToday>(temp);
}
else if (args[0] == "apod_date")
{
    INasaClient<DateTime, Task<MediaOfToday>> client;
    client = new DateNasaClient(root["ApiKey"]);
    if (!DateTime.TryParseExact(args[1], "yyyy-mm-dd", new CultureInfo("en-US"),
            DateTimeStyles.None, out DateTime date))
        return ErrorMassage($"{args[1]}: wrong argument format");

    Task<MediaOfToday> res = client.GetAsync(date);
    MediaOfToday temp = await res;
    result.Add(temp);
}
else if (args[0] == "apod_last")
{
    INasaClient<int, Task<MediaOfToday[]>> client;
    client = new LastDaysNasaClient(root["ApiKey"]);
    if (!int.TryParse(args[1], out int N))
        return ErrorMassage($"{args[1]}: wrong argument format");

    Task<MediaOfToday[]> res = client.GetAsync(N);
    MediaOfToday[] temp = await res;
    result = new List<MediaOfToday>(temp);
}
else if (args[0] == "apod_range")
{
    if (args.Length < 3)
        return ErrorMassage("Wrong number of arguments to input");
    INasaClient<KeyValuePair<DateTime, DateTime>, Task<MediaOfToday[]>> client;
    client = new RangeNasaClient(root["ApiKey"]);
    if (!DateTime.TryParseExact(args[1], "yyyy-MM-dd", new CultureInfo("en-US"),
            DateTimeStyles.None, out DateTime start) ||
        !DateTime.TryParseExact(args[2], "yyyy-MM-dd", new CultureInfo("en-US"),
            DateTimeStyles.None, out DateTime end))
        return ErrorMassage($"{args[1]}, {args[2]}: wrong arguments format");
    
    var range = new KeyValuePair<DateTime, DateTime>(start,end);
    Task<MediaOfToday[]> res = client.GetAsync(range);
    MediaOfToday[] temp = await res;
    result = new List<MediaOfToday>(temp);
}
else
    return ErrorMassage($"{args[0]}: client not found");

foreach (var el in result)
{
    Console.WriteLine(el);
    Console.WriteLine();
}

int ErrorMassage(string message)
{
    Console.WriteLine(message);
    return -1;
}
return 1;
