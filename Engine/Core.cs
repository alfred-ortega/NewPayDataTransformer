using System;
using System.Collections.Generic;
using NewPayDataTransformer.Engine;
using NewPayDataTransformer.Model;

namespace NewPayDataTransformer.Engine
{
    public class Core
    {
        MockEmployeeDb mockEmployeeDb;
        public void Execute()
        {
            loadConfig();
            loadEmployees();
        }

        private void loadConfig()
        {

        }

        private void loadEmployees()
        {
            string employeefile = @"D:\Shared\NEWPAY\PAR_OUTBOUND\16FEB2019\PDW_EDS_MASTER_GS16FEB2019.dat";
            EmployeeLoader loader = new EmployeeLoader(employeefile);
            EmployeeValidator validator = new EmployeeValidator(loader);
            validator.Validate();
            mockEmployeeDb = new MockEmployeeDb(validator.GetEmployees(new DateTime(),"GS"));
        }

        
    }//end class
}//end namespace