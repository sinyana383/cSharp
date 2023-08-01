using System.Globalization;

namespace d02_ex00.Models
{
    public struct ExchangeSum
    {
        public string Identifier;
        public double Amount;

        public ExchangeSum(string identifier, double amount)
        {
            this.Identifier = identifier;
            this.Amount = amount;
        }

        public static bool ParsingData(string data, out ExchangeSum exSum)
        {
            exSum = new ExchangeSum();

            if (data == null)
                return false;
            string[] strs = data.Split();
            if (strs.Length != 2)
                return false;
        
            if (!double.TryParse(strs[0], out exSum.Amount))
                return false;
            exSum.Identifier = strs[1];
            return true;
        }

        public override string ToString()
        {
            return $"{Amount.ToString("N2", new CultureInfo("en-GB"))}" +
                   $" {Identifier}";
        }
    }
}