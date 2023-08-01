using System;
using System.Globalization;

var curCult = new CultureInfo("en-GB");
// DateTime curDate = DateTime.Now;
// For checking 1st and 2nd examples from the Task
var curDate = new DateTime(2021, 05, 15);

double sum;
double rate;
int term;
//  Errors checks
if (args.Length < 3)
    return ErrorMassage();
if (!(double.TryParse(args[0], out sum) && double.TryParse(args[1], out rate) && int.TryParse(args[2], out term)))
    return ErrorMassage();
if (sum <= 0 || rate <= 0 || term <= 0)
    return ErrorMassage();

double i = rate / (12 * 100);
double payment = (sum * i * Math.Pow(1 + i, term)) / (Math.Pow(1 + i, term) - 1);
for (int no = 1; no <= term; no++)
{
    DateTime payDate = new DateTime(curDate.AddMonths(no).Year, curDate.AddMonths(no).Month, 1);
    double interest = sum * rate * DateTime.DaysInMonth(payDate.Year, payDate.AddMonths(-1).Month) 
                      / (100 * (DateTime.IsLeapYear(payDate.Year) ? 366 : 365) );
    double principaldebt = payment - interest;
    if (no == term)
    {
        principaldebt = sum;
        payment = principaldebt + interest;
    }

    Console.WriteLine($"{no, -8}" +
                      $"{payDate.ToString("MM/dd/yyyy", curCult), -16}" +
                      $"{payment.ToString("N2", curCult), -16}" +
                      $"{principaldebt.ToString("N2", curCult), -16}" +
                      $"{interest.ToString("N2", curCult), -16}" +
                      $"{(sum - principaldebt).ToString("N2", curCult)}");

    sum -= principaldebt;
}

static int ErrorMassage()
{
    Console.WriteLine("Something went wrong. Check your input and retry.");
    return -1;
}

return 1;

// (input parsing, string interpolation, DateTime, CultureInfo)