using System;


class A
{
    public virtual void Print()
    {
        System.Console.WriteLine("A implementation");
    }
}

class B : A
{
    public override void Print()
    {
        System.Console.WriteLine("B implementation");
    }
}

class C : A
{
    public override void Print()
    {
        System.Console.WriteLine("C implementation");
    }
}

class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("hello");
    }
}