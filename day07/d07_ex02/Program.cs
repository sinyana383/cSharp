using System;
using d07_ex02.ConsoleSetter;
using d07_ex02.Models;

var u = new IdentityUser();
var u1 = new IdentityUser();
var r = new IdentityRole();
var consoleSetter = new ConsoleSetter();

consoleSetter.SetValues(u);
Console.WriteLine();
consoleSetter.SetValues(u1);
Console.WriteLine();
consoleSetter.SetValues(r);