using System;
using System.Collections.Generic;
using System.Linq;
using NewPayDataTransformer.Model;

namespace NewPayDataTransformer.Engine
{
    public class MockEmployeeDb
    {
        List<Employee> employees;
        SortedList<string,string> SSNMapping;

        public MockEmployeeDb(List<Employee> emps)
        {
            employees = emps;
            SSNMapping = new SortedList<string, string>();
            setMapping();
        }

        private void setMapping()
        {
            foreach(Employee emp in employees)
            {
                SSNMapping.Add(emp.Ssn,emp.MockSSN);
            }
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