using System;

namespace d_01
{

    public class Customer
    {
        // static fields
        static Random s_r = new Random();

        // auto-properties:
        public string Name { get; }
        public int SerialNum { get; }
        public int GoodsNumInCart { get; private set; }


        public Customer(string name, int serialNum)
        {
            Name = name;
            SerialNum = serialNum;
            GoodsNumInCart = 0;
        }

        public override string ToString() => Name + ", Customer #" + SerialNum;
        public static bool operator ==(Customer c1, Customer c2) => 
            (c1 is null && c2 is null) || (c1 is not null && c1.Equals(c2));
        public static bool operator !=(Customer c1, Customer c2) => !(c1 == c2);

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (Customer)obj;
            return this.Name == other.Name && this.SerialNum == other.SerialNum;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, SerialNum);
        }

        public void FillCart(int cartCapacity)
        {
            if (cartCapacity < 1) return;

            GoodsNumInCart = s_r.Next(1, cartCapacity + 1);
        }
    }
}