using System;
using System.Reflection;
using Microsoft.AspNetCore.Http;

DefaultHttpContext instance = new DefaultHttpContext();
Console.WriteLine("Old Response value: " + instance.Response);

FieldInfo myFieldInfo;

myFieldInfo = typeof(DefaultHttpContext)
    .GetField("_response", BindingFlags.NonPublic | BindingFlags.Instance);
if (myFieldInfo == null)
    return -1;
myFieldInfo.SetValue(instance, null);
Console.WriteLine("New Response value: " + instance.Response);
return 0;