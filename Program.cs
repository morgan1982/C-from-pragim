using System;
using System.Collections.Generic;

public delegate void HelloFunc(string Message);


class Program
{
    public static void Main(string[] args)
    {
        List<Employee> empList = new List<Employee>();

        empList.Add(new Employee() { ID = 101, Name = "Tom", Salary = 5000, Experience = 5});
        empList.Add(new Employee() { ID = 102, Name = "Sara", Salary = 4000, Experience = 4});
        empList.Add(new Employee() { ID = 103, Name = "Max", Salary = 6000, Experience = 6});
        empList.Add(new Employee() { ID = 104, Name = "Jerry", Salary = 3000, Experience = 3});

        IsPromotable prm = new IsPromotable(PromoteEmp);



    }

    public static bool PromoteEmp(Employee emp)
    {
        if (emp.Experience >= 5)
        {
            return true;
        }else {
            return false;
        }
    }

}

delegate bool IsPromotable(Employee empl);
class Employee
{
    public int ID{ get;set; }
    public string Name{ get;set; }
    public int Salary{ get;set; }
    public int Experience{ get;set; }

    public static void PromoteEmployee(List<Employee> employeeList, IsPromotable readyForPromo)
    {
        foreach(Employee employee in employeeList)
        {
            if (readyForPromo(employee))
            {
                System.Console.WriteLine(employee.Name + " promoted");
            }
        }
    }

}