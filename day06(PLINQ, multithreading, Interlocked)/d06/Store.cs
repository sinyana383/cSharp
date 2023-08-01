using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace d_01
{
    public class Store
    {
        public bool AnyCustomers = true;
        private  ConcurrentBag<CashRegister> _cashRegistersSet;
        public Storage Storage;

        private Thread[] _registersThreads;
        public IEnumerable<CashRegister> CashRegistersSet => _cashRegistersSet;
        public int CashRegisterNumber => _cashRegistersSet.Count;

        public Store(int storageCapacity, int numberOfRegisters, 
            TimeSpan itemSpend, TimeSpan switchSpend)
        {
            Storage = new Storage(storageCapacity);

            _cashRegistersSet = new ConcurrentBag<CashRegister>();
            for (var i = 1; i <= numberOfRegisters; ++i)
                _cashRegistersSet.Add(new CashRegister("Register #" + i,
                    itemSpend, switchSpend));
        }

        public bool IsOpen() => !Storage.IsEmpty;

        public void OpenRegisters()
        {
            _registersThreads = new Thread [CashRegisterNumber];

            int i = -1;
            foreach (var reg in _cashRegistersSet)
                _registersThreads[++i] = new Thread(() => reg.Work(this));

            foreach (var th in _registersThreads)
                th.Start();
        }

        public void CloseRegisters()
        {
            if (_registersThreads is null)
                return;
            foreach (var th in _registersThreads)
                th.Join();

            foreach (var reg in _cashRegistersSet)
            {
                double average = reg.AllSpend.TotalSeconds / 
                                 (reg.CustomersPassed > 0 ? reg.CustomersPassed : 1);
                Console.WriteLine($"{reg}, average time: {average, 0:N2} sec");
            }
        }

    }
}