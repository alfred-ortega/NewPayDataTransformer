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
                    tempEmployee.FirstName = data[2];
                    tempEmployee.MiddleName = data[3];
                    tempEmployee.LastName = data[4];
                    tempEmployee.DateOfBirth = DateTime.Parse(data[6]);
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