using System.Globalization;
using System.Linq;

namespace d02_ex00.Models
{
    public struct ExchangeRate
    {
        public string CurrencyFrom;
        public string CurrencyTo;
        public double Exchange;
    
        public static bool ParsingData(string data, out ExchangeRate exRate)
        {
            exRate = new ExchangeRate();
            if (data == null)
                return false;
        
            var whitespaceChars = data.Where(char.IsWhiteSpace);
            string sep = " " + string.Join("", data.Where(c=> char.IsWhiteSpace(c) 
                                                              || (!char.IsDigit(c) && !char.IsLetter(c) && c != ',' && c != '.')));
            string[] strs = data.Split(sep.ToCharArray());
            string[] result = strs.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            if (result.Length != 3)
                return false;
        
            if (!double.TryParse(result[2], NumberStyles.Float, 
                    CultureInfo.GetCultureInfo("ru-RU"), out exRate.Exchange))
                return false;
            exRate.CurrencyFrom = result[0];
            exRate.CurrencyTo = result[1];
            return true;
        }

        public override string ToString()
        {
            return $"{CurrencyFrom} - {CurrencyTo}: " +
                   $"{Exchange.ToString("N2", new CultureInfo("en-GB"))}";
        }
    }
}
