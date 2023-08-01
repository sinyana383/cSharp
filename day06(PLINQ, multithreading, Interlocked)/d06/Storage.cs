using System.Threading;

namespace d_01
{
    public class Storage
    {
        private int Capacity { get; }
        public int GoodsNum => _goodsNum;
        private int _goodsNum;

        public Storage(int capacity)
        {
            if (capacity < 0)
                capacity = 0;

            Capacity = capacity;
            _goodsNum = Capacity;
        }

        public bool IsEmpty => _goodsNum <= 0;

        public int TakeGoods(int num)
        {
            if (num > _goodsNum)
                num = _goodsNum;

            Interlocked.Add(ref _goodsNum, -num);
            return num;
        }
    }
}