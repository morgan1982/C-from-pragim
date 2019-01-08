using System;


interface ICustomer
{
    void Print();
}
interface ICustomer2 : ICustomer
{
    void Print2();
}

class Customer : ICustomer2
{
    public void Print()
    {
        System.Console.WriteLine("Interface print method");
    }
    public void Print2()
    {
        System.Console.WriteLine("Interface Print 2");
    }

}

public class Program
{
    public static void Main()
    {
        ICustomer C1 = new Customer();
        // only the print method of the first interface is available
        C1.Print();
    }
}
