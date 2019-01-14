using System;

public class Customer
{
    public string name { get; set; }
    public int gender { get; set; }

    public static string GetGender(int gen)
    {
        switch(gen)
        {
            case 0:
                return "Unknown";
            case 1:
                return "Male";
            case 2:
                return "Female";
            default:
                return "invalid data";
        }
    }
}


class Program
{
    public static void Main(string[] args)
    {

        Customer[] customers = new Customer[3];

        customers[0] = new Customer
        {
            name = "Mark",
            gender = 1
        };
        customers[1] = new Customer
        {
            name = "tom",
            gender = 1
        };
        customers[2] = new Customer
        {
            name = "tonya",
            gender = 2
        };

        foreach (var customer in customers)
        { 
            System.Console.WriteLine("name: {0} genre: {1}", customer.name, Customer.GetGender(customer.gender));
        }




    }
}