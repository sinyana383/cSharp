using System.Collections.Generic;
using System.IO;
using System.Linq;
using d02_ex00.Models;

namespace d02_ex00
{
    public class Exchanger
    {
        public List<ExchangeRate> ExchangeRates;

        public Exchanger() => ExchangeRates = new List<ExchangeRate>();
    
        public static bool ParsingData(string ratesDirectory, out Exchanger ex)
        {
            ex = new Exchanger();
            if (!Directory.Exists(ratesDirectory))
                return false;
        
            var stockExchangerFiles = Directory.GetFiles(ratesDirectory);
            foreach (var file in stockExchangerFiles)
            {
                string[] strs = File.ReadAllLines(file);
                string name = Path.GetFileNameWithoutExtension(file);
                foreach (var str in strs)
                {
                    ExchangeRate exRate;
                    if (!ExchangeRate.ParsingData(name + "->" + str, out exRate))
                        return false;
                    ex.ExchangeRates.Add(exRate);
                }
            }
            return true;
        }
    
        public List<ExchangeSum> Convert(ExchangeSum originalSum)
        {
            var convertedList = new List<ExchangeSum>();
            List<ExchangeRate> rates = ExchangeRates
                .Where(r => r.CurrencyFrom == originalSum.Identifier).ToList();
            foreach (var r in rates)
            {
                convertedList.Add(new ExchangeSum(
                    r.CurrencyTo, 
                    originalSum.Amount * r.Exchange));
            }

            return convertedList;
        }
    }
}