using System;
using System.Reflection;
using Microsoft.AspNetCore.Http;


Console.WriteLine($"Type: {typeof(DefaultHttpContext)}");
Console.WriteLine($"Assembly: {Assembly.GetAssembly(typeof(DefaultHttpContext))}");
Console.WriteLine($"Based on: {typeof(DefaultHttpContext).BaseType}");
Console.WriteLine();


Console.WriteLine("Fields:");
FieldInfo[] myFieldInfo;
myFieldInfo = typeof(DefaultHttpContext).GetFields(BindingFlags.Instance | BindingFlags.Static
                                                                         | BindingFlags.NonPublic | BindingFlags.Public);
for(int i = 0; i < myFieldInfo.Length; i++)
    Console.WriteLine($"{myFieldInfo[i].FieldType} {myFieldInfo[i].Name}");
Console.WriteLine();


Console.WriteLine("Properties:");
PropertyInfo [] myPropertyInfo;
myPropertyInfo = typeof(DefaultHttpContext).GetProperties(BindingFlags.Instance | BindingFlags.Static 
                                                                            | BindingFlags.Public);
for(int i = 0; i < myPropertyInfo.Length; i++)
    Console.WriteLine($"{myPropertyInfo[i].PropertyType} {myPropertyInfo[i].Name}");
Console.WriteLine();


Console.WriteLine("Methods:");
MethodInfo [] myMethodInfo;
myMethodInfo = typeof(DefaultHttpContext).GetMethods(BindingFlags.Instance | BindingFlags.Static 
                                                                    | BindingFlags.Public);
for (int i = 0; i < myMethodInfo.Length; i++)
{
    Console.Write($"{myMethodInfo[i].ReturnType.Name} {myMethodInfo[i].Name} (");
    ParameterInfo[] myPinfo = myMethodInfo[i].GetParameters();
    for (int j = 0; j < myPinfo.Length; j++)
    {
        Console.Write($"{myPinfo[j].ParameterType.Name} {myPinfo[j].Name}");
        if (j < myPinfo.Length - 1)
            Console.WriteLine(", ");
    }
    Console.WriteLine(")");
}