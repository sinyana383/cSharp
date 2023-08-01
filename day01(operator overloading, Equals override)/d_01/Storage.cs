namespace d_01
{
    public class Storage
    {
        private int Capacity { get; }
        public int GoodsNum { get; private set; }

        public Storage(int capacity)
        {
            if (capacity < 0)
                capacity = 0;

            Capacity = capacity;
            GoodsNum = Capacity;
        }

        public bool IsEmpty => GoodsNum <= 0;

        public int TakeGoods(int num)
        {
            if (num > GoodsNum)
                num = GoodsNum;

            GoodsNum -= num;
            return num;
        }
    }
}