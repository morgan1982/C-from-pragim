using System;

namespace polymorphism
{
    class Employee
    {
        private int _id;
        public int Id
        {
            set{
                if (value < 0)
                {
                    throw new Exception("negative number");
                }
                this._id = value;
            }
            get{
                return this._id;
            }
        } 
    }

    class Program
    {
        static void Main(string[] args)
        {
            Employee max = new Employee();
            max.Id = 12;
            System.Console.WriteLine(max.Id);

            Employee george = max;
            george.Id = 13;
            System.Console.WriteLine(max.Id);

        }
    }
}
