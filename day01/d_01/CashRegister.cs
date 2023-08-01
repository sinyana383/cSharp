using System.Collections.Generic;

namespace d_01
{

    public class CashRegister
    {
        public string Name { get; }
        public int CustomersNumber => _queueCheckout.Count;

        private Queue<Customer> _queueCheckout;

        public CashRegister(string name)
        {
            Name = name;
            _queueCheckout = new Queue<Customer>();
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

        // public IEnumerable<Customer> Customers => _queueCheckout;
        public void AddCustomerToCheckout(Customer c) => _queueCheckout.Enqueue(c);

        public int GetGoodsNumberFromQueue()
        {
            var res = 0;

            foreach (Customer c in _queueCheckout)
                res += c.GoodsNumInCart;
            return res;
        }

    }
}