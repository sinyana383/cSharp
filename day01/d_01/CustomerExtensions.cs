using System.Collections.Generic;
using System.Linq;

namespace d_01
{
    public static class CustomerExtensions
    {
        public static CashRegister LeastCustomersNumber(HashSet<CashRegister> setOfRegisters)
        {
            if (setOfRegisters.Count < 1)
                return null;

            CashRegister minCustomers = setOfRegisters.First();
            foreach (CashRegister cur in setOfRegisters)
            {
                if (cur.CustomersNumber < minCustomers.CustomersNumber)
                    minCustomers = cur;
            }

            return minCustomers;
        }

        public static CashRegister LeastGoodsNumber(HashSet<CashRegister> setOfRegisters)
        {
            if (setOfRegisters.Count < 1)
                return null;

            CashRegister minGoods = setOfRegisters.First();
            foreach (CashRegister cur in setOfRegisters)
            {
                if (cur.GetGoodsNumberFromQueue() < minGoods.GetGoodsNumberFromQueue())
                    minGoods = cur;
            }

            return minGoods;
        }
    }
}