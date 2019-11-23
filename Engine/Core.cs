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
        List<Employee> employees;
        MockEmployeeDb mockEmployeeDb;
        public void Execute()
        {
           
            loadEmployees();
            loadUpdatedEmployees();
            loadEmployeeEftPayments();
            loadEmployeeEftAddresses();
            loadNonEFT();
            //loadMappingFiles();
            if(Config.Settings.Action == "Mask")
            {
                //executeMasking();
            }
            else
            {
                //executeUnmasking();
            }
        }

        private void loadEmployees()
        {
            Logger.Log.Record("Begin Core.loadEmployees");
            string employeefile = Config.Settings.EmployeeFile;
            Logger.Log.Record("loading employees from " + Config.Settings.EmployeeFile);
            EmployeeLoader loader = new EmployeeLoader(employeefile);
            Logger.Log.Record("EmployeeLoader action complete");
            EmployeeValidator validator = new EmployeeValidator(loader);
            Logger.Log.Record("Employeevalidator action complete");
            validator.Validate();
            Logger.Log.Record("End Core.loadEmployees");
        }

        private void loadUpdatedEmployees()
        {
            
            NewPayContext context = new NewPayContext();
            Logger.Log.Record("Begin Core.loadUpdatedEmployees");
            List<Employee> emps = context.Employee.Where(e => e.Agency == Config.Settings.Agency).ToList();
            Logger.Log.Record(emps.Count.ToString() + " records loaded");
            mockEmployeeDb = new MockEmployeeDb( emps  );
            Logger.Log.Record("End Core.loadUpdatedEmployees");

        }

        private void loadEmployeeEftPayments()
        {
            Logger.Log.Record("Begin Core.loadEmployeeEftPayments");
            EmployeeEftPaymentLoader eftLoader = new EmployeeEftPaymentLoader(mockEmployeeDb);
            Logger.Log.Record("End Core.loadEmployeeEftPayments");
        }

        private void loadEmployeeEftAddresses()
        {
            //EmployeeEftAddressLoader
            Logger.Log.Record("Begin Core.loadEmployeeEftAddresses");
            EmployeeEftAddressLoader eftLoader = new EmployeeEftAddressLoader(mockEmployeeDb);
            Logger.Log.Record("End Core.loadEmployeeEftAddresses");
        }

        private void loadNonEFT()
        {//EmployeeNonEftPaymentLoader
            Logger.Log.Record("Begin Core.EmployeeNonEftPaymentLoader");
            EmployeeNonEftPaymentLoader eftLoader = new EmployeeNonEftPaymentLoader(mockEmployeeDb);
            Logger.Log.Record("End Core.EmployeeNonEftPaymentLoader");

        }
        
    }//end class
}//end namespace