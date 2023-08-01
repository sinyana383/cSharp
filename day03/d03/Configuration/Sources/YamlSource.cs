using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace d03.Configuration.Sources
{
    public class YamlSource : IConfigurationSource
    {
        public int Priority { get; private set; }
    
        private readonly string _filePath;
        private Hashtable _hashtable;

        public YamlSource(string filePath, int priority)
        {
            Priority = priority;
            _filePath = filePath;
            _hashtable = LoadData();
        }

        public Hashtable LoadData()
        {
            try
            {
                string yString = File.ReadAllText(_filePath);
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                var dictionary = deserializer.Deserialize<Dictionary<string, object>>(yString);
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