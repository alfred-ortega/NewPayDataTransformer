using System;
using System.Collections.Generic;
using System.Linq;
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
            loadUpdatedEmployees();
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
        }

        private void loadUpdatedEmployees()
        {
            NewPayContext context = new NewPayContext();

            mockEmployeeDb = new MockEmployeeDb( context.Employee.Where(e => e.Agency == Config.Settings.Agency).ToList()  );

        }

        
    }//end class
}//end namespace