using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Employees
{
   public class Employee
    {
        ArrayList employeeList;
        public Employee(string csv)
        {
            try
            {
                employeeList = filterCSVToArray(csv);
                ValidateSalaries(employeeList);
                ValidateEmployReportTo(employeeList);
            }

            catch (Exception e)
            {
                throw e;
            }
        }


        /* This method returns csv array list converted from string
         */

        public ArrayList filterCSVToArray(string csv)
        {
            ArrayList cleanedData = new ArrayList();
            if (string.IsNullOrEmpty(csv) || !(csv is string))
            {
                throw new Exception("csv cannot be null");

            }

            /**
             *Format CSV to Row .
             */

            string[] datarows = csv.Split(
            new[] { Environment.NewLine },
            StringSplitOptions.None
            );


            /**
             *  Rows into an array 
             **/

            foreach (string row in datarows)
            {
                string[] data = row.Split(',');
                ArrayList filteredData = new ArrayList();

                foreach (string cell in data)
                {
                    filteredData.Add(cell);
                }
                if (filteredData.Count != 3)
                {
                    throw new Exception("CSV value must have 3 values in each row");
                }
                cleanedData.Add(filteredData);
            }

            return cleanedData;
        }

        /*
         *1. The salaries in the CSV are valid integer numbers. 
         */

        public void ValidateSalaries(ArrayList employeesList)
        {
            foreach (ArrayList employee in employeesList)
            {
                string employeeSalary = Convert.ToString(employee[2]);
                int number;
                if (!(int.TryParse(employeeSalary, out number)))
                {
                    throw new Exception("Employees salaries must be a valid integer");
                }
            }
        }

        /*
         * 2. One employee does not report to more than one manager. 
         */

        public void ValidateEmployReportTo(ArrayList employees)
        {
            ArrayList savedEmployees = new ArrayList();
            ArrayList managers = new ArrayList();
            ArrayList ceo = new ArrayList();
            ArrayList Employee = new ArrayList();

            foreach (ArrayList employee in employees)
            {
                string employeename = employee[0] as string;
                string managername = employee[1] as string;

                if (savedEmployees.Contains(employeename.Trim()))
                {
                    throw new Exception("Employee report one manager");
                }

                savedEmployees.Add(employeename.Trim());

                if (!string.IsNullOrEmpty(managername.Trim()))
                {
                    managers.Add(managername.Trim());
                }
                else
                {
                    ceo.Add(employeename.Trim());
                }

            }

            // 3. There is only one CEO, i.e. only one employee with no manager. 

            int managersDiff = employees.Count - managers.Count;
            if (managersDiff != 1)
            {
                throw new Exception("There is only one CEO");
            }

            // 5. There is no manager that is not an employee, i.e. all managers are also listed in the employee  column. 
            foreach (string manager in managers)
            {
                if (!savedEmployees.Contains(manager.Trim()))
                {
                    throw new Exception("All Manager should be employees ce");
                }
            }


            // Add a junior employees
            foreach (string employee in savedEmployees)
            {
                if (!managers.Contains(employee) && !ceo.Contains(employee))
                {
                    Employee.Add(employee.Trim());
                }
            }

            ////// check for circular reference
            for (var i = 0; i < employees.Count; i++)
            {
                var employeeData = employees[i] as ArrayList;
                var employeeManager = employeeData[1] as string;
                int index = savedEmployees.IndexOf(employeeManager);

                if (index != -1)
                {
                    var managerData = employees[index] as ArrayList;
                    var topManager = managerData[1] as string;

                    if ((managers.Contains(topManager.Trim()) && !ceo.Contains(topManager.Trim()))
                        || Employee.Contains(topManager.Trim()))
                    {
                        throw new Exception("Circular reference error");
                    }
                }
            }


        }

        /**
  *
  *Return salary budgets of a specified manager
  * **/

        public int managerSalaryBudget(string manageName)
        {
            int totalManagerSalary = 0;
            foreach (ArrayList employee in employeeList)
            {
                var name = employee[1] as string;
                var employeeSalary = employee[2] as string;
                var employeName = employee[0] as string;
                if (name.Trim() == manageName.Trim() || employeName.Trim() == manageName.Trim())
                {
                    totalManagerSalary += Convert.ToInt32(employeeSalary);
                }
            }
            return totalManagerSalary;
        }

    }
}
