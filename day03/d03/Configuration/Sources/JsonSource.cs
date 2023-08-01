using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace d03.Configuration.Sources
{
    public class JsonSource : IConfigurationSource
    {
        public int Priority { get; private set; }

        private readonly string _filePath;
        private Hashtable _hashtable;

        public JsonSource(string filePath, int priority)
        {
            Priority = priority;
            _filePath = filePath;
            _hashtable = LoadData();
        }

        public Hashtable LoadData()
        {
            try
            {
                string jString = File.ReadAllText(_filePath);
                Dictionary<string, object> dictionary = 
                    JsonSerializer.Deserialize<Dictionary<string, object>>(jString);
                if (dictionary != null)
                    return new Hashtable(dictionary);
            }
            catch
            {
                Console.WriteLine("Invalid data. Check your input and try again.");
            }

            return new Hashtable();
        }

        public Hashtable GetParameters() => _hashtable;
    }
}