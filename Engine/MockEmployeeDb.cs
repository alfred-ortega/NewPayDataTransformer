using System;
using System.Collections.Generic;
using System.Linq;
using NewPayDataTransformer.Model;

namespace NewPayDataTransformer.Engine
{
    public class MockEmployeeDb
    {
        List<Employee> employees;
        Dictionary<string,string> SSNMapping;

        public MockEmployeeDb(List<Employee> emps)
        {
            employees = emps;
            setMapping();
        }

        private void setMapping()
        {
            SSNMapping = new Dictionary<string, string>();
            foreach(Employee emp in employees)
            {
                try
                {
                    SSNMapping.Add(emp.Ssn,emp.MockSSN);
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            SSNMapping.OrderBy(i => i.Key);
            //
            //
        }

        public string GetSSN(string mockSSN)
        {

            try
            {
                string retval = string.Empty;
                retval = (from d in SSNMapping where d.Value == mockSSN select d).First().Key;
                return retval;                
            }
            catch (System.Exception)
            {
                throw new ArgumentOutOfRangeException("Record for that SSN could not be found.");
            }
        }

        public Employee GetEmployee(string mockSSN, string agency)
        {
            Employee emp = employees.Where(e => e.MockSSN == mockSSN && e.Agency == agency).Single();
            return emp;
        }

        public Employee GetEmployeeBySSN(string Ssn, string agency)
        {
            Employee emp = employees.Where(e => e.Ssn == Ssn && e.Agency == agency).Single();
            return emp;
        }



        public string GetMockSSN(string ssn)
        {
            try
            {
                string retval = string.Empty;
                retval = SSNMapping[ssn];
                return retval;                
            }
            catch (System.Exception)
            {
                throw new ArgumentOutOfRangeException("Record for that SSN could not be found.");
            }
        }

        public MockEmployee GetMockEmployee(string ssn, string agency)
        {
            Employee emp = employees.Where(e => e.Ssn == ssn && e.Agency == agency).Single();
            return emp.GetMockedEmployee();
        }

    }//end class
}//end namespace