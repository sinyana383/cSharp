using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using d_01;
using d06;
using Microsoft.Extensions.Configuration;

//Init
const int customersNb = 10;
const int storageCapacity = 50;
const int registersNb = 4;
const int cartCapacity = 7;

IConfiguration root;
try
{
    string baseDirectory = Directory.GetCurrentDirectory();
    string fullPath = Path.Combine(baseDirectory, "appsettings.json");
    if (!File.Exists(fullPath))
        return ErrorMassage($"{fullPath} not found");
    IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddJsonFile(fullPath);
    root = configurationBuilder.Build();
}
catch
{
    Console.WriteLine("Something went wrong with json file");
    return -1;
}
if (!int.TryParse(root["item_spend"], out var itemSpend) || itemSpend < 1
    || !int.TryParse(root["switch_spend"], out var switchSpend) || switchSpend < 1)
    return ErrorMassage("Wrong json parameters");

var customers = new ConcurrentBag<Customer>();
Store s = new Store(storageCapacity, registersNb,
    TimeSpan.FromSeconds(itemSpend), TimeSpan.FromSeconds(switchSpend));
Console.WriteLine("Less people mode");
for (int i = 0; i < customersNb; ++i)
    customers.Add(new Customer("Chmonya", i + 1));
OpenStore(QueueChooseMode.LessPeople, s, customers);

var customers2 = new ConcurrentBag<Customer>();
Store s2 = new Store(storageCapacity, registersNb,
    TimeSpan.FromSeconds(itemSpend), TimeSpan.FromSeconds(switchSpend));
Console.WriteLine("\nLess goods mode");
for (int i = 0; i < customersNb; ++i)
    customers2.Add(new Customer("Erjan", i + 1));
OpenStore(QueueChooseMode.LessGoods, s2, customers2);

// local functions
void OpenStore(QueueChooseMode mode, Store store, ConcurrentBag<Customer> customerSet)
{
    Customer.Mode = mode;
    Parallel.ForEach(customerSet, c =>
    {
        c.FillCart(cartCapacity, store.Storage);
        c.GetInLine(store.CashRegistersSet);
    });

    store.OpenRegisters();
    int j = 0;
    Thread.Sleep(7000);
    while (store.IsOpen())
    {
        store.AnyCustomers = true;
        var c = new Customer("Noname", ++j);
        c.FillCart(cartCapacity, store.Storage);
        c.GetInLine(store.CashRegistersSet);
        Thread.Sleep(7000);
    }
    store.AnyCustomers = false;
    store.CloseRegisters();
}

int ErrorMassage(string message)
{
    Console.WriteLine(message);
    return -1;
}
return 1;