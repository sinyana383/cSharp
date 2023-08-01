using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using d04.Model;
using Microsoft.Extensions.Configuration;

// Load
string baseDirectory = Directory.GetCurrentDirectory();
string fullPath = Path.Combine(baseDirectory, "appsettings.json");

IConfigurationBuilder configurationBuilder =
    new ConfigurationBuilder().AddJsonFile(fullPath);
IConfiguration root = configurationBuilder.Build();

var movieReviews = new List<MovieReview>();
var bookReviews = new List<BookReview>();

foreach (var element in GetResults(root["movie_reviews"]).EnumerateArray())
    movieReviews.Add(JsonSerializer.Deserialize<MovieReview>(element.ToString()));

foreach (var element in GetResults(root["book_reviews"]).EnumerateArray())
    bookReviews.Add(JsonSerializer.Deserialize<BookReview>(element.ToString()));

// Best
if (args.Length > 0 && args[0].Equals("best"))
{
    Console.WriteLine("Best in books:");
    Console.WriteLine(bookReviews.Best());
    Console.WriteLine();
    
    Console.WriteLine("Best in movie reviews:");
    Console.WriteLine(movieReviews.Best());

    return;
}

// Search
Console.WriteLine("> Input search text:");
var input = Console.ReadLine() ?? string.Empty;
var booksFound = bookReviews.Search(input);
var moviesFound = movieReviews.Search(input);

if (!booksFound.Any() && !moviesFound.Any())
{
    Console.WriteLine($"There are no \"{input}\" in media today.");
    return;
}

Console.WriteLine($"\nItems found: {booksFound.Count() + moviesFound.Count()}\n");
if (booksFound.Any())
{
    Console.WriteLine($"Book search result [{booksFound.Count()}]:");
    var output = booksFound.OrderByDescending(b => b.Title);
    foreach (var el in output)
        Console.WriteLine(el);
    Console.WriteLine();
}
if (moviesFound.Any())
{
    Console.WriteLine($"Movie search result [{moviesFound.Count()}]:");
    var output = moviesFound.OrderBy(b => b.Title);
    foreach (var el in output)
        Console.WriteLine(el);
    Console.WriteLine();
}

// Local functions
JsonElement GetResults(string path)
{
    string json = File.ReadAllText(path);
    JsonElement jsonArray = JsonDocument.Parse(json)
        .RootElement.GetProperty("results");
    return jsonArray;
}