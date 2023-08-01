using System;
using System.Collections.Generic;
using d_01;

/*
 * auto-properties, static fields, lambda operators
 * top-level-statements, override the ToString()
 */

/* ex00 */
Console.WriteLine("--ex00--");

var s = new Storage(10);
Console.WriteLine(s.GoodsNum);
Console.WriteLine();

/* ex01 */
Console.WriteLine("--ex01--");

var customer1 = new Customer("Andrew", 1);
var customer2 = new Customer("Andrew", 1);

Console.WriteLine(customer1);
Console.WriteLine(customer2);
if (customer1 == customer2)
    Console.WriteLine(customer1 == customer2);
Console.WriteLine();

/* ex02 */
Console.WriteLine("--ex02--");

var customer3 = new Customer("Ergan", 1);
var customer4 = new Customer("Natasha", 2);
var customer5 = new Customer("Povar", 3);

customer3.FillCart(15);
customer4.FillCart(15);
customer5.FillCart(15);

Console.WriteLine(customer3 + " (" + customer3.GoodsNumInCart + " items in cart)");
Console.WriteLine(customer4 + " (" + customer4.GoodsNumInCart + " items in cart)");
Console.WriteLine(customer5 + " (" + customer5.GoodsNumInCart + " items in cart)");
Console.WriteLine();


/* ex03 */
Console.WriteLine("--ex03--");

var customer32 = new Customer("Ergan", 1);
var customer42 = new Customer("Natasha", 2);
var customer52 = new Customer("Povar", 3);

customer32.FillCart(15);
customer42.FillCart(15);
customer52.FillCart(15);

var сashRegister1 = new CashRegister("Register #1");
var сashRegister12 = new CashRegister("Register #1");
var сashRegister2 = new CashRegister("Register #2");

сashRegister1.AddCustomerToCheckout(customer32);
сashRegister1.AddCustomerToCheckout(customer42);
сashRegister2.AddCustomerToCheckout(customer52);

Console.WriteLine($"{сashRegister1} - {сashRegister1.CustomersNumber} customer(s)");
Console.WriteLine($"{сashRegister2} - {сashRegister2.CustomersNumber} customer(s)");
Console.WriteLine(сashRegister1 == сashRegister12);
Console.WriteLine();

/* ex04 */
Console.WriteLine("--ex04--");

Store a = new Store(15,10);
Console.WriteLine(a.IsOpen());
Console.WriteLine();

/* ex05 */
Console.WriteLine("--ex05--");

Store a2 = new Store(160,4);

HashSet<CashRegister> cashRegisters = (HashSet<CashRegister>)a2.CashRegistersSet;
int customerNb = 10;
for (int p = 1; p <= customerNb; p++)
{
    var customer = new Customer("noname", p);
    customer.FillCart(15);
    CashRegister cashRegister = CustomerExtensions.LeastCustomersNumber(cashRegisters);
    if (cashRegister is null)
        return ErrorMassage("CashRegistersSet is empty or null");
    cashRegister.AddCustomerToCheckout(customer);
    Console.WriteLine($"{customer} ({customer.GoodsNumInCart} items in cart) to {cashRegister}");
}

foreach (var c in a2.CashRegistersSet)
    Console.WriteLine($"{c.Name}: {c.CustomersNumber} customers and {c.GetGoodsNumberFromQueue()}");

Console.WriteLine("The Least Customer Number on " + CustomerExtensions.LeastCustomersNumber(cashRegisters));
Console.WriteLine("The Least Goods Number on " + CustomerExtensions.LeastGoodsNumber(cashRegisters));
Console.WriteLine();

/* ex06 */
Console.WriteLine("--ex06--");

const int goodsInStore = 40;
const int numberOfRegisters = 3;
const int numberOfCustomers = 10;
const int cartCapacity = 7;

var store1 = new Store(goodsInStore, numberOfRegisters);
var customersSet1 = new HashSet<Customer>(numberOfCustomers);
for (var i = 1; i <= numberOfCustomers; ++i)
    customersSet1.Add(new Customer("Noname", i));
    
Console.WriteLine("LeastCustomersMode");
store1.Work(customersSet1, cartCapacity, CustomerExtensions.LeastCustomersNumber);
Console.WriteLine();


var store2 = new Store(goodsInStore, numberOfRegisters);
var customersSet2 = new HashSet<Customer>(numberOfCustomers);
for (var i = 1; i <= numberOfCustomers; ++i)
    customersSet2.Add(new Customer("Noname", i));

Console.WriteLine("LeastGoodsMode");
store2.Work(customersSet2, cartCapacity, CustomerExtensions.LeastGoodsNumber);
Console.WriteLine();


// local functions
int ErrorMassage(string message)
{
    Console.WriteLine(message);
    return -1;
}

return 1;
