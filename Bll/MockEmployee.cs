using System;
using System.Collections.Generic;

namespace NewPayDataTransformer.Model
{
    public class MockEmployee : IEmployee
    {
        public string Agency {get; private set;}
        public string Ssn {get; private set;}
        public string LastName {get; private set;}
        public string FirstName {get; private set;}
        public string MiddleName {get; private set;}
        public string Suffix {get; private set;}
        public DateTime DateOfBirth { get; private set;}
        public string StreetAddress {get; private set;}
        public string StreetAddress2 {get; private set;}
        public string StreetAddress3 {get; private set;}
       
        public string City {get; private set;}
        public string State {get; private set;}
        public string ZipCode {get; private set;}
        public string ZipCode2 {get; private set;}
        public DateTime PayPeriodEndDate {get; private set;}

        private List<MockEmployeeEft> eftRecords;
        public List<MockEmployeeEft> EftRecords
        {
            get { return eftRecords; }
        }

        private List<MockEmployeeNonEft> nonEftRecords;
        public List<MockEmployeeNonEft> NonEftRecords
        {
            get { return nonEftRecords; }
        }
        
        

        public string FullName {get {return string.Format("{0}, {1} {2}",this.LastName,this.FirstName, this.MiddleName ).Trim();}}

        public string FullZipCode { get {return string.Format("{0}-{1}",ZipCode,ZipCode2);}}


        public MockEmployee(Employee employee)
        {
            setInitValues(employee.Id.ToString(), employee.PayPeriodEndDate, employee.Agency, employee.DateOfBirth);
            eftRecords = new List<MockEmployeeEft>();
            nonEftRecords = new List<MockEmployeeNonEft>();
            foreach(Employeeeft eft in employee.Employeeeft)
            {
                MockEmployeeEft m = new MockEmployeeEft(eft.Id,eft.PayPeriodEndDate);
                eftRecords.Add(m);
            }
            foreach(Employeenoneft noneft in employee.Employeenoneft)
            {
                MockEmployeeNonEft m = new MockEmployeeNonEft(noneft.Id, noneft.PayPeriodEndDate);
                nonEftRecords.Add(m);
            }
        }


        public void setInitValues(string mockId, DateTime payPeriodEndDate, string agency, DateTime dateOfBirth)
        {
            this.Agency = agency;
            this.PayPeriodEndDate = payPeriodEndDate;
            this.Ssn = "1001" + mockId;
            this.DateOfBirth = dateOfBirth; // setDateOfBirth(dateOfBirth, payPeriodEndDate);
            this.LastName = "BAINES" + mockId;
            this.FirstName = "ROBERT";
            this.MiddleName = string.Empty;
            this.Suffix = string.Empty;
            this.StreetAddress = string.Format("1{0} Mockingbird Lane",mockId);
            this.StreetAddress2 = string.Empty;
            this.StreetAddress3 = string.Empty;
            this.City = "KANSAS CITY";
            this.State = "MO";
            this.ZipCode = "64108";
            this.ZipCode2 = string.Empty;
        }

        private DateTime setDateOfBirth(DateTime dateOfBirth,DateTime payPeriodEndDate)
        {
            DateTime retval = dateOfBirth;
            DateTime payPeriodStartDate = payPeriodEndDate.AddDays(-14);
            if(dateOfBirth >= payPeriodStartDate && dateOfBirth <= payPeriodEndDate )
                retval = payPeriodStartDate;

            return retval;    
        }


        
    }//end class
}//end namespace