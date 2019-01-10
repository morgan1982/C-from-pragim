using System;
using System.IO;



class Program
{
    public static void Main(string[] args)
    {

        try{
            System.Console.WriteLine("enter first number");
            int fn = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("enter second number");
            int sn = Convert.ToInt32(Console.ReadLine());

            int result = fn / sn;
            System.Console.WriteLine(result);
        }
        catch (Exception ex)
        {
            string filePath = @"C:\Sample Files\Log.txt";
            StreamWriter sw = new StreamWriter(filePath, append:true);
            sw.Write("{0}\n", ex.GetType().Name);
            sw.Write(ex.StackTrace);
            sw.Close();
            System.Console.WriteLine("there is a problem");
        }



    }



}

