using System;
using Employees;
namespace EmployeeMain
{
    class Program
    {

        static void Main(string[] args)
        {
            Employee employees = new Employee(
                "Emplyee4,Eployee2,2500" +
                "\n" +
                "Employee3,Employee1,800" +
                "\n" +
                "Employee1,,1000" +   // ceo
                "\n" +
                "Employee5,Employee1,1500" +
                "\n" +
                "Employee2,Employee1,500"
                );

            int salary = employees.managerSalaryBudget("Emplyee4");

            Console.WriteLine(salary);
        }
    }
}
