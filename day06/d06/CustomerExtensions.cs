using System;
using System.Collections.Generic;
using System.Linq;
using d_01;

namespace d06
{
    public static class CustomerExtensions
    {
        public static CashRegister GetInLine(this Customer customer,
            IEnumerable<CashRegister> registers)
        {
            if (Customer.Mode == QueueChooseMode.LessGoods)
                return customer.GetInLineByItems(registers);
            
            return customer.GetInLineByPeople(registers);
        }

        public static CashRegister GetInLineByPeople(this Customer customer,
            IEnumerable<CashRegister> registers)
        {
            var random = new Random();
            CashRegister register = registers.Aggregate((currentMin, x) =>
                (x.CustomersNumber < currentMin.CustomersNumber || 
                x.CustomersNumber == currentMin.CustomersNumber && random.Next(2) == 0)
                    ? x : currentMin);
            register?.AddCustomerToCheckout(customer);

            return register;
        }

        public static CashRegister GetInLineByItems(this Customer customer,
            IEnumerable<CashRegister> registers)
        {
            var random = new Random();
            CashRegister register = registers.Aggregate((currentMin, x) =>
                (x.GetGoodsNumberFromQueue() < currentMin.GetGoodsNumberFromQueue() || 
                 x.GetGoodsNumberFromQueue() == currentMin.GetGoodsNumberFromQueue() && random.Next(2) == 0)
                    ? x : currentMin);
            register?.AddCustomerToCheckout(customer);

            return register;
        }
    }
}