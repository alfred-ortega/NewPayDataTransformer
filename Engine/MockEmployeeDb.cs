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
                catch (System.ArgumentException)
                {
                    //
                }
                catch (System.Exception x)
                {
                    Logger.Log.Record(LogType.Error,x.ToString());
                    throw x;
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
            //Employee emp = employees.Where(e => e.Ssn == Ssn && e.Agency == agency).Single();
            Employee emp = employees.Where(e => e.Ssn == Ssn).Single();
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
                string message = "(MockEmployeeDb.GetMockSSN) Record for SSN {" + ssn  + "} could not be found in agency " + Config.Settings.Agency;
                Logger.Log.Record(LogType.Error, message);
                throw new ArgumentOutOfRangeException(message);
            }
        }

        public MockEmployee GetMockEmployee(string ssn)
        {
            //Employee emp = employees.Where(e => e.Ssn == ssn && e.Agency == agency).Single();
            try
            {
                Employee emp = employees.Where(e => e.Ssn == ssn).Single();
                return emp.GetMockedEmployee();
            }
            catch (System.Exception x)
            {
                if(ssn == "000000000" || ssn.Substring(0,1) == "Z")
                {
                    return new MockEmployee(ssn);
                }
                else
                {
                    string message = "(MockEmployeeDb.GetMockEmployee) Record for SSN {" + ssn  + "} could not be found in agency " + Config.Settings.Agency;
                    Logger.Log.Record(LogType.Error, string.Format("{0} - {1}",message,x.ToString()) ) ;
                    return new MockEmployee("999999999");
                }
            }
        }

    }//end class
}//end namespace