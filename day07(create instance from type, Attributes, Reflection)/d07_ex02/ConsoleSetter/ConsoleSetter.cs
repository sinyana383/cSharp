using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using d07_ex02.Attributes;
using Microsoft.AspNetCore.Http;

namespace d07_ex02.ConsoleSetter
{
    public class ConsoleSetter
    {
        public void SetValues<T>(T input) where T : class
        {
            Console.WriteLine($"Let's set {typeof(T).Name}!");
            
            // Properties
            PropertyInfo [] myPropertyInfo;
            myPropertyInfo = typeof(T).GetProperties(BindingFlags.Instance 
                | BindingFlags.Public);
            
            for (int i = 0; i < myPropertyInfo.Length; i++)
            {
                var noDisplayAttribute = 
                    myPropertyInfo[i].GetCustomAttribute<NoDisplayAttribute>();
                if (noDisplayAttribute is not null)
                    continue;
                var descriptionAttribute = 
                    myPropertyInfo[i].GetCustomAttribute<DescriptionAttribute>();
                var defaultAttribute = 
                    myPropertyInfo[i].GetCustomAttribute<DefaultValueAttribute>();
                
                string desc = descriptionAttribute is not null
                    ? descriptionAttribute.Description
                    : myPropertyInfo[i].Name;
                string defaultVal = defaultAttribute?.Value != null 
                    ? defaultAttribute?.Value.ToString()
                    : string.Empty;
                
                Console.WriteLine($"Set {desc}:");
                var userInput = Console.ReadLine();
                if (string.IsNullOrEmpty(userInput))
                        userInput = defaultVal;
                    
                myPropertyInfo[i].SetValue(input, userInput);
            }
            
            Console.WriteLine("\nWe've set our instance!");
            Console.WriteLine(input);
        }
    }
}