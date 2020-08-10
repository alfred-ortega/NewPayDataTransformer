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
                string[] data = rows[i].Split("\"|\""); // string[] data = rows[i].Split("\",\"");  Change comma to pipe
                try
                {
                    TempEmployee tempEmployee = new TempEmployee();
                    tempEmployee.Emplid = data[1];
                    tempEmployee.Agency = Config.Settings.Agency;
                    tempEmployee.FirstName = data[2];
                    tempEmployee.MiddleName = data[4];
                    tempEmployee.LastName = data[5];
                    tempEmployee.DateOfBirth = data[14];
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