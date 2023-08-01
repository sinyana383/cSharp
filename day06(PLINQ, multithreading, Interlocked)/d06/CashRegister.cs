using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace d_01
{

    public class CashRegister
    {
        public string Name { get; }
        private ConcurrentQueue<Customer> _queueCheckout;
        public int CustomersNumber => _queueCheckout.Count;
        public int CustomersPassed { get; private set; }
        public TimeSpan ItemSpend { get; }
        public TimeSpan SwitchSpend { get; }
        public TimeSpan AllSpend { get; private set; }


        public CashRegister(string name, TimeSpan itemSpend, TimeSpan switchSpend)
        {
            Name = name;
            _queueCheckout = new ConcurrentQueue<Customer>();
            CustomersPassed = 0;
            ItemSpend = itemSpend;
            SwitchSpend = switchSpend;
            AllSpend = TimeSpan.Zero;
        }

        public void Work(Store s)
        {
            while (_queueCheckout.Any() || s.IsOpen() || s.AnyCustomers)
                Process();
        }
        private void Process()
        {
            if (!_queueCheckout.TryDequeue(out Customer c))
                return;
            
            ++CustomersPassed;
            for (int i = 0; i < c.GoodsNumInCart; ++i)
                Thread.Sleep(new Random().Next(1, (int)ItemSpend.TotalSeconds) * 1000);
            Thread.Sleep(new Random().Next(1, (int)SwitchSpend.TotalSeconds) * 1000);

            AllSpend += new TimeSpan(0, 0,
                c.GoodsNumInCart * (int)ItemSpend.TotalSeconds + (int)SwitchSpend.TotalSeconds);
            Console.WriteLine($"{this}: {c}({c.GoodsNumInCart} goods) – {CustomersNumber} customers left");
        }
        
        public void AddCustomerToCheckout(Customer c) => _queueCheckout.Enqueue(c);
        public int GetGoodsNumberFromQueue()
        {
            var res = 0;

            foreach (Customer c in _queueCheckout)
                res += c.GoodsNumInCart;
            return res;
        }

        public static bool operator ==(CashRegister c1, CashRegister c2) => 
            (c1 is null && c2 is null) || (c1 is not null && c1.Equals(c2));
        public static bool operator !=(CashRegister c1, CashRegister c2) => !(c1 == c2);
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            var other = (CashRegister)obj;
            return this.Name == other.Name;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
        public override string ToString() => Name;
    }
}