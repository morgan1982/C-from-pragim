using System;

public abstract class Customer
{
    public abstract void Print();
}


public class Program : Customer
{

    public override void Print()
    {
        System.Console.WriteLine("print the abstract now");
    }
    public static void Main(string[] args)
    {
        Program P = new Program();
        P.Print();
    }
}
