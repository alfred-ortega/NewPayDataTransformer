using System;
using System.Collections.Generic;
using System.Linq;
using NewPayDataTransformer.Model;

namespace  NewPayDataTransformer.Engine
{
    public class EmployeeValidator
    {
        NewPayContext context;
        List<Employee> existingEmployees;
        List<TempEmployee> newEmployees;


        public EmployeeValidator(EmployeeLoader empLoader)
        {
            newEmployees = empLoader.TempEmployees;
            context = new NewPayContext();
        }

        public void Validate()
        {
            setExistingEmployees();
            addOrUpdateEmployees();
        }

        public List<Employee> GetEmployees(DateTime payPeriodEndDate, string agency)
        {
            return context.Employee.Where(e => e.PayPeriodEndDate == payPeriodEndDate && e.Agency == agency).ToList();

        }

        private void setExistingEmployees()
        {
            existingEmployees = context.Employee.ToList();
        }

        private void addOrUpdateEmployees()
        {
            Console.WriteLine("Starting loop at: " + DateTime.Now.ToShortTimeString());
            int newEmployeeCount = 0;
            int updatedEmployeeCount = 0;
            int existingEmployeeCount = context.Employee.Where(e => e.Agency == Config.Settings.Agency).Count();
            Console.WriteLine(string.Format("There are {0} employees in the db",existingEmployeeCount));
            foreach(TempEmployee newEmployee in newEmployees)
            {
                Employee existingEmployee = context.Employee.Where(e => e.Ssn == newEmployee.Ssn).Single();
                if(existingEmployee==null)//new record
                {
                    context.Employee.Add(newEmployee.CreateEmployee());
                    newEmployeeCount++;
                }
                else
                {
                    //WE DO NOT CHANGE THE SSN
                    existingEmployee.Agency = newEmployee.Agency;
                    existingEmployee.LastName = newEmployee.LastName;
                    existingEmployee.FirstName = newEmployee.FirstName;
                    existingEmployee.MiddleName = newEmployee.MiddleName;
                    existingEmployee.Suffix = newEmployee.Suffix;
                    existingEmployee.DateOfBirth = newEmployee.DateOfBirth;
                    existingEmployee.StreetAddress = newEmployee.StreetAddress;
                    existingEmployee.StreetAddress2 = newEmployee.StreetAddress2;
                    existingEmployee.StreetAddress3 = newEmployee.StreetAddress3;
                    existingEmployee.City = newEmployee.City;
                    existingEmployee.State = newEmployee.State;
                    existingEmployee.ZipCode = newEmployee.ZipCode;
                    existingEmployee.ZipCode2 = newEmployee.ZipCode2;
                    existingEmployee.PayPeriodEndDate = newEmployee.PayPeriodEndDate;
                    context.Update(existingEmployee);
                    updatedEmployeeCount++;
                }
            }
            Console.WriteLine(string.Format("New Employees: {0}\tUpdated Employees {1} ",newEmployeeCount,updatedEmployeeCount));
            Console.WriteLine("Finished loop at: " + DateTime.Now.ToShortTimeString());
            Console.WriteLine("Starting DB Save at: " + DateTime.Now.ToShortTimeString());
            context.SaveChanges();
            Console.WriteLine("Finshed DB Save at: " + DateTime.Now.ToShortTimeString());

        }


    }//end class
}//end namespace
