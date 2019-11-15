using System;
using System.Collections.Generic;
using System.IO;
using NewPayDataTransformer.Model;

namespace NewPayDataTransformer.Engine
{
    public class EmployeeLoader
    {
        public List<TempEmployee> TempEmployees { get; set; }
        public EmployeeLoader(string employeeFile)
        {
            if(!File.Exists(employeeFile))
                throw new FileNotFoundException(string.Format("The File '{0}' was not found.",employeeFile));

            TempEmployees = new List<TempEmployee>();                

            string[] rows = File.ReadAllLines(employeeFile); 
            foreach(string row in rows)
            {
                string[] data = row.Split("~");
                try
                {
                    TempEmployee tempEmployee = new TempEmployee();
                    tempEmployee.Agency = data[0];
                    tempEmployee.Ssn = data[1];
                    tempEmployee.FirstName = data[2];
                    tempEmployee.MiddleName = data[3];
                    tempEmployee.LastName = data[4];
                    tempEmployee.Suffix = data[5];
                    tempEmployee.DateOfBirth = DateTime.Parse(data[6]);
                    tempEmployee.City = data[7];
                    tempEmployee.StreetAddress = data[8];
                    tempEmployee.StreetAddress2 = data[9];
                    tempEmployee.State = data[10];
                    tempEmployee.ZipCode = data[11];
                    tempEmployee.ZipCode2 = data[12];
                    tempEmployee.PayPeriodEndDate = DateTime.Parse(data[13]);
                    TempEmployees.Add(tempEmployee);                    
                }
                catch (System.Exception x)
                {
                    
                    throw;
                }


            }               
            
        }

    }//end class
}//end namespace