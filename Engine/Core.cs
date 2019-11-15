using System;
using System.Collections.Generic;
using NewPayDataTransformer.Engine;
using NewPayDataTransformer.Model;

namespace NewPayDataTransformer.Engine
{
    public class Core
    {
        public void Execute()
        {
            loadEmployees();

        }

        private void loadEmployees()
        {
            string employeefile = @"D:\Shared\NEWPAY\PAR_OUTBOUND\16FEB2019\PDW_EDS_MASTER_GS16FEB2019.dat";
            EmployeeLoader loader = new EmployeeLoader(employeefile);
            EmployeeValidator validator = new EmployeeValidator(loader);
            validator.Validate();
        }
    }//end class
}//end namespace