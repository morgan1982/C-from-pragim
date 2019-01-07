using System;

namespace polymorphism
{
    public class Employee
    {
        public string FirstName = "Max";
        public string LastName = "Goodman";

        private float salary;

        public void SetSalary(float amount)
        {
            if (amount < 0)
            {
                throw new Exception("Salary has to be positive");
            }
            this.salary = amount;
        }
        public float GetSalary()
        {
            if (this.salary > 0)
            {
                return salary;
            }else {
                System.Console.WriteLine("there is 0 amount");
                return 0;
            }
        }

        public virtual void PrintFullName()
        {
            System.Console.WriteLine("{0} {1}", this.FirstName, this.LastName);
        }
    }
    public class PartTimeEmployee : Employee
    {
        public override void PrintFullName()
        {
            System.Console.WriteLine("{0} {1} --part time", this.FirstName, this.LastName);
        }

    }
    public class FullTimeEmployee : Employee
    {
        public override void PrintFullName()
        {
            System.Console.WriteLine("{0} {1} --fulltime", this.FirstName, this.LastName);
        }
    }
    public class TemporaryEmployee : Employee
    {
        public override void PrintFullName()
        {
            System.Console.WriteLine("{0} {1} --temp", this.FirstName, this.LastName);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            // polymorpshism enables the invocation of derived class methods through baseclass reference variables
            // in runtime
            Employee[] employees = new Employee[4]; 

            employees[0] = new Employee();
            employees[1] = new PartTimeEmployee();
            employees[2] = new FullTimeEmployee();
            employees[3] = new TemporaryEmployee();

            foreach(Employee e in employees)
            {
                e.PrintFullName();
            }

            Employee max = new Employee();
            max.SetSalary(34.5F);
            float salary = max.GetSalary();
            System.Console.WriteLine("max salary is: {0}", salary);
        }
    }
}
