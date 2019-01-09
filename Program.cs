using System;
using System.Collections.Generic;


public delegate void SampleDelegate();
class Program
{
    public static void Main(string[] args)
    {

        // using the same instance
        SampleDelegate del = new SampleDelegate(SampleMethodOne);
        del += SampleMethodTwo;

        del();

    }
    public static void SampleMethodOne()
    {
        System.Console.WriteLine("method 1 envoked");
    }
    public static void SampleMethodTwo()
    {
        System.Console.WriteLine("method 2 envoked");
    }
    public static void SampleMethodThree()
    {
        System.Console.WriteLine("method 3 envoked");
    }



}

