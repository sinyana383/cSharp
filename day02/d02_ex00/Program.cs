using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using d02_ex00;
using d02_ex00.Models;

var curCult = new CultureInfo("en-GB");
Exchanger exchanger;
ExchangeSum origSum;

//  Errors checks
if (args.Length < 2)
    return ErrorMassage();
if (!ExchangeSum.ParsingData(args[0], out origSum))
    return ErrorMassage();
if (!Exchanger.ParsingData(args[1], out exchanger))
    return ErrorMassage();

List<ExchangeSum> list = exchanger.Convert(origSum);
if (!list.Any())
    return ErrorMassage();
Console.WriteLine($"Amount in the original currency: {origSum.ToString()}");
foreach (var sums in list)
    Console.WriteLine($"Amount in {sums.Identifier}: {sums.ToString()}");

// local functions
static int ErrorMassage()
{
    Console.WriteLine("Input error. Check the input data and repeat the request.");
    return -1;
}

return 1; 