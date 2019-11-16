using System;
using System.Collections.Generic;
using NewPayDataTransformer.Engine;
using NewPayDataTransformer.Model;
using Newtonsoft.Json;

namespace NewPayDataTransformer.Engine
{
    public class Core
    {
        MockEmployeeDb mockEmployeeDb;
        public void Execute()
        {
            Console.WriteLine(Config.Settings.EmployeeFile);
            loadEmployees();
            //loadEFT();
            //loadNonEFT();
            //loadMappingFiles();
            if(Config.Settings.Action == "Mask")
            {
                //executeMasking();
            }
            else
            {
                //executeUnmasking();
            }
            string ssn = "574942760";
            string mockSSN = mockEmployeeDb.GetMockSSN(ssn);
            Console.WriteLine(string.Format("{0} has the mock SSN of {1}",ssn,mockSSN));
        }

        private void loadEmployees()
        {
            string employeefile = Config.Settings.EmployeeFile;
            EmployeeLoader loader = new EmployeeLoader(employeefile);
            EmployeeValidator validator = new EmployeeValidator(loader);
            validator.Validate();
            mockEmployeeDb = new MockEmployeeDb(validator.GetEmployees(new DateTime(),Config.Settings.Agency));
        }

        
    }//end class
}//end namespace