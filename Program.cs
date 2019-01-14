using System;




class Program
{
    public static void Main(string[] args)
    {
        int numerator;
        bool isNumenatorSuccess = Int32.TryParse(Console.ReadLine(), out numerator);

        if (isNumenatorSuccess)
        {
            int denominator;
            System.Console.WriteLine("Enter denominator\n");
            bool isDenominatorSuccess = Int32.TryParse(Console.ReadLine(), out denominator);

            if (isDenominatorSuccess && denominator != 0)
            {
                int result = numerator / denominator;
                System.Console.WriteLine("result is {0}", result);
            }
            else
            {
                if (denominator == 0)
                {
                    System.Console.WriteLine("denominator cannot be 0");
                }
                else{
                    System.Console.WriteLine("denominator has to be between {0} and {1}", Int32.MinValue, Int32.MinValue);
                }
            }
        }
        else
        {
            System.Console.WriteLine("numerator has to be between {0} and {1}", Int32.MinValue, Int32.MaxValue);
        }

    }
}