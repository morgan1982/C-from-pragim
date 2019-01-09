using System;
using System.Collections.Generic;


public delegate void SampleDelegate();
class Program
{
    public static void Main(string[] args)
    {

        SampleDelegate delOne, delTwo, delThree, delFour;
        delOne = new SampleDelegate(SampleMethodOne);
        delTwo = new SampleDelegate(SampleMethodTwo);
        delThree = new SampleDelegate(SampleMethodThree);

        delFour = delOne + delThree;

        delFour();

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

