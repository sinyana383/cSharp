using System;
using System.IO;
using d03.Configuration;
using d03.Configuration.Sources;

/*Console.WriteLine("-- ex01 --");
JsonSource jSource;
Configuration c;

if (args.Length < 1 || !File.Exists(args[0]))
    return ErrorMassage();

jSource = new JsonSource(args[0]);
c = new Configuration(jSource);

Console.WriteLine(c);
Console.WriteLine();

Console.WriteLine("-- ex02 --");
YamlSource ySource;
Configuration c1;

if (args.Length < 1 || !File.Exists(args[0]))
    return ErrorMassage();

ySource = new YamlSource(args[0]);
c1 = new Configuration(ySource);

Console.WriteLine(c1);
Console.WriteLine();*/

if (args.Length < 4 
    || !int.TryParse(args[1], out var jsonPriority) 
    || !int.TryParse(args[3], out var yamlPriority)
    || !File.Exists(args[0]) || !File.Exists(args[2]))
    return ErrorMassage();

var jSource = new JsonSource(args[0], jsonPriority);
var c = new Configuration(jSource);

var ySource = new YamlSource(args[2], yamlPriority);
c.LoadMoreConfigData(ySource);
Console.WriteLine(c);

static int ErrorMassage()
{
    Console.WriteLine("Input error. Check the input data and repeat the request.");
    return -1;
}

return 1;
