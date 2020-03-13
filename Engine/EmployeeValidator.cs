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

        public List<Employee> GetEmployees()
        {
            return context.Employee.ToList();

        }

        private void setExistingEmployees()
        {
            existingEmployees = new List<Employee>();
            try
            {
                int i = context.Employee.Count();
                if(i > 0)
                    existingEmployees = context.Employee.ToList();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private void addOrUpdateEmployees()
        {
            Console.WriteLine("Starting loop at: " + DateTime.Now.ToShortTimeString());
            int newEmployeeCount = 0;
            int updatedEmployeeCount = 0;
            int i = 0;
            int existingEmployeeCount = existingEmployees.Count();
            Console.WriteLine(string.Format("There are {0} employees in the db",existingEmployeeCount));
            foreach(TempEmployee newEmployee in newEmployees)
            {
                Employee existingEmployee = context.Employee.Where(e => e.Emplid == newEmployee.Emplid).SingleOrDefault();
                if (existingEmployee==null)//new record
                {
                    context.Employee.Add(newEmployee.CreateEmployee());
                    context.SaveChanges();                        
                    newEmployeeCount++;
                }
                else
                {
                    //WE DO NOT CHANGE THE Emplid
                    existingEmployee.Agency = newEmployee.Agency;
                    existingEmployee.LastName = newEmployee.LastName;
                    existingEmployee.FirstName = newEmployee.FirstName;
                    existingEmployee.MiddleName = newEmployee.MiddleName;
                    existingEmployee.DateOfBirth = newEmployee.DateOfBirth;
                    context.Update(existingEmployee);
                    context.SaveChanges();                    
                    updatedEmployeeCount++;
                }
                i++;
                if(i % 100 == 0)
                {
                    Console.WriteLine(string.Format("{0} {1} db records inserted/updated",DateTime.Now.ToString(), i));
                }
            }
            Console.WriteLine(string.Format("New Employees: {0}\tUpdated Employees {1} ",newEmployeeCount,updatedEmployeeCount));
            Console.WriteLine("Finished loop at: " + DateTime.Now.ToString());
            Console.WriteLine("Starting DB Save at: " + DateTime.Now.ToString());
            Console.WriteLine("Finshed DB Save at: " + DateTime.Now.ToString());
        }
    }//end class
}//end namespace
