using System;


public delegate void HelloFunc(string Message);


class Program
{
    public static void Main(string[] args)
    {
        // a deligate is a type safe function pointer
        HelloFunc del = new HelloFunc(Hello);
        del("Hello from delegate");
    }

    public static void Hello(string strMessage)
    {
        System.Console.WriteLine(strMessage);
    }
}