using System;
using System.Collections.Generic;
using System.Linq;

namespace d_01
{
    public class Store
    {
        private Storage _storage;
        private HashSet<CashRegister> _cashRegistersSet;

        public IEnumerable<CashRegister> CashRegistersSet => _cashRegistersSet;
        public int CashRegisterNumber => _cashRegistersSet.Count;

        public Store(int storageCapacity, int numberOfRegisters)
        {
            _storage = new Storage(storageCapacity);

            _cashRegistersSet = new HashSet<CashRegister>(numberOfRegisters);
            for (var i = 1; i <= numberOfRegisters; ++i)
                _cashRegistersSet.Add(new CashRegister("Register #" + i));
        }

        public bool IsOpen() => !_storage.IsEmpty;

        public void Work(HashSet<Customer> customers, int cartCapacity,
            Func<HashSet<CashRegister>, CashRegister> queueSelection)
        {

            while (IsOpen() && customers.Any())
            {
                Customer c = customers.First();
                customers.Remove(c);

                if (_storage.GoodsNum < cartCapacity)
                    cartCapacity = _storage.GoodsNum;
                c.FillCart(cartCapacity);
                _storage.TakeGoods(c.GoodsNumInCart);
                if (_storage.IsEmpty && c.GoodsNumInCart > 0)
                    Console.WriteLine($"{c} ({c.GoodsNumInCart} items left in cart)");

                CashRegister cashReg = queueSelection(_cashRegistersSet);
                if (cashReg is null)
                {
                    Console.WriteLine("CashRegistersSet is empty or null");
                    return;
                }

                Console.WriteLine($"{c} ({c.GoodsNumInCart} items in cart) - {cashReg} ({cashReg.CustomersNumber}" +
                                  $" people with {cashReg.GetGoodsNumberFromQueue()} items behind)");
                cashReg.AddCustomerToCheckout(c);
            }
        }

    }
}