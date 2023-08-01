using System;
using System.IO;
using System.Linq;

if(!File.Exists("us_names.txt"))
    return ErrorMassage("Dictionary file not found");
string[] names = File.ReadLines("us_names.txt").ToArray();
for (int i = 0; i < names.Length; ++i)
    names[i] = names[i].Trim();

Console.WriteLine(">Enter name:");
string input = Console.ReadLine();
if (input == null)
    return ErrorMassage("Your name was not found");
if (!input.All(c => Char.IsLetter(c) || c == '-' || c == ' '))
    return ErrorMassage("Something went wrong. Check your input and retry.");
input = input.Trim();

for (int i = 0; i < names.Length; i++)
{
    if (names[i] == input)
    {
        Console.WriteLine($"Hello, {names[i]}!");
        return 1;   
    }
}

for (int i = 0; i < names.Length; i++)
{
    int edits = LevenshteinDistance(input, names[i]);
    
    if (edits < 2)
    {
        Console.WriteLine($"Did you mean “{names[i]}”? Y/N");
        string response = Console.ReadLine();
        if (response == null || (response != "Y" && response != "N"))
            return ErrorMassage("Something went wrong. Check your input and retry.");

        if (response == "N")
            continue;
        
        Console.WriteLine($"Hello, {names[i]}!");
        return 1;
    }
}

return ErrorMassage("Your name was not found.");

// local functions
int ErrorMassage(string message)
{
    Console.WriteLine(message);
    return -1;
}

int LevenshteinDistance(string s1, string s2)
{
    if (s1.Length == 0)
        return s2.Length;
    if (s2.Length == 0)
        return s1.Length;
    
    int[,] array = new int[s1.Length + 1, s2.Length + 1];
    int add;
    
    for (int c = 0; c < s2.Length + 1; c++)
        array[0, c] = c;
    for (int r = 0; r < s1.Length + 1; r++)
        array[r, 0] = r;

    for (int i = 1; i < s1.Length + 1; i++)
    {
        for (int j = 1; j < s2.Length + 1; j++)
        {
            add = s1.ToCharArray()[i - 1] != s2.ToCharArray()[j - 1] ? 1 : 0;
            
            array[i, j] = Min(array[i - 1, j] + 1, array[i, j - 1] + 1
                , array[i - 1, j - 1] + add);
        }
    }

    return array[s1.Length, s2.Length];
}

int Min(int a, int b, int c)
{
    int min = a;

    if (min > b)
        min = b;
    if (min > c)
        min = c;

    return min;
}