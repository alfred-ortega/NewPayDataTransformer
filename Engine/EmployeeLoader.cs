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
            int rowCount = rows.Length;
            for(int i = 1; i < rowCount; i++) // skip header row so we start at 1
            {
                string[] data = rows[i].Split(",");
                try
                {
                    TempEmployee tempEmployee = new TempEmployee();
                    tempEmployee.Emplid = data[3];
                    tempEmployee.Agency = data[1];
                    tempEmployee.FirstName = data[4];
                    tempEmployee.MiddleName = data[6];
                    tempEmployee.LastName = data[7];
                    tempEmployee.DateOfBirth = data[18];
                    TempEmployees.Add(tempEmployee);                    
                }
                catch (System.Exception x)
                {
                    throw x;
                }
            }
            
        }

    }//end class
}//end namespace