using System;
using d07_ex03.Models;
using d07_ex03;

var uС = TypeFactory<IdentityUser>.CreateWithConstructor();
var uA = TypeFactory<IdentityUser>.CreateWithActivator();
Console.WriteLine(typeof(IdentityUser));
Console.WriteLine($"user1 {(uС == uA ? "==" : "!=")} user2");

var rС = TypeFactory<IdentityRole>.CreateWithConstructor();
var rA = TypeFactory<IdentityRole>.CreateWithActivator();
Console.WriteLine(typeof(IdentityRole));
Console.WriteLine($"role1 {(rС == rA ? "==" : "!=")} role2");

Console.WriteLine($"\n{typeof(IdentityUser)}");
Console.WriteLine("Set name:");
var parametersStr = Console.ReadLine();
object[] parameters = { parametersStr };
var u = TypeFactory<IdentityUser>.CreateWithParameters(parameters);
Console.WriteLine($"Username set: {u.UserName}");