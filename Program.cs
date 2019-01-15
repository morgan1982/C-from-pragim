using System;

public class Customer
{
    #region Fields
    private int _id;
    private string _firstName;
    private string _lastName;
    #endregion

    public string name { get; set; }
    public string gender { get; set; }

}

public enum Gender
{
    Male,
    Female,
    Unknown
}


class Program
{
    public static void Main(string[] args)
    {

        Customer[] customers = new Customer[3];

        customers[0] = new Customer
        {
            name = "Mark",
            gender = Enum.GetName(typeof(Gender), Gender.Male)
        };
        customers[1] = new Customer
        {
            name = "tom",
            gender = Enum.GetName(typeof(Gender), Gender.Male)
        };
        customers[2] = new Customer
        {
            name = "tonya",
            gender = Enum.GetName(typeof(Gender), Gender.Female)
        };

        foreach (var customer in customers)
        { 
            System.Console.WriteLine("name: {0} genre: {1}", customer.name, customer.gender);
        }




    }
}