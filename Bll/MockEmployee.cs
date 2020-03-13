using System;
using System.Collections.Generic;

namespace NewPayDataTransformer.Model
{
    public class MockEmployee :BaseMockObject, IEmployee
    {
        public string Agency {get; private set;}
        public string Emplid {get; private set;}
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

        public string EmployeeId {get {return Emplid.Substring(4);} }
        public string BeneficiarySSN {get; private set;}
        public string BeneficiaryName {get; private set;}
        public string BeneficiaryStreetAddress {get; private set;}
      
        public string BeneficiaryCity {get; private set;}
        public string BeneficiaryState {get; private set;}
        public string BeneficiaryZipCode {get; private set;}
        public string BeneficiaryZipCode2 {get; private set;}

        public string HBIPayeeName {get; private set;}
        public string HBIPayeeSSN {get; private set;}



        
        

        public string FullName {get {return string.Format("{0}, {1} {2}",this.LastName,this.FirstName, this.MiddleName ).Trim();}}

        public string FullZipCode { get {return string.Format("{0}-{1}",ZipCode,ZipCode2);}}


        public MockEmployee(Employee employee)
        {
            setInitValues(this.LPad(employee.Id.ToString(),5), employee.PayPeriodEndDate, employee.Agency, employee.DateOfBirth);
            //eftRecords = new List<MockEmployeeEft>();
            //nonEftRecords = new List<MockEmployeeNonEft>();
            // foreach(Employeeeft eft in employee.Employeeeft)
            // {
            //     MockEmployeeEft m = new MockEmployeeEft(eft.Id,eft.PayPeriodEndDate,employee.MockSSN);
            //     eftRecords.Add(m);
            // }
            // foreach(Employeenoneft noneft in employee.Employeenoneft)
            // {
            //     MockEmployeeNonEft m = new MockEmployeeNonEft(noneft.Id, noneft.PayPeriodEndDate, employee.MockSSN);
            //     nonEftRecords.Add(m);
            // }

        }

        public MockEmployee(string ssn)
        {
            this.Emplid = ssn;
            
        }


        public void setInitValues(string mockId, DateTime payPeriodEndDate, string agency, DateTime dateOfBirth)
        {
            this.Agency = agency;
            this.PayPeriodEndDate = payPeriodEndDate;
            this.Emplid = "1001" + mockId;
            this.DateOfBirth = dateOfBirth; 
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
            this.BeneficiarySSN = "2002" + mockId;
            this.BeneficiaryName = "Fred Rogers" + mockId;
            this.BeneficiaryStreetAddress = mockId + " Sesame St";
            this.BeneficiaryCity = "Mayberry";
            this.BeneficiaryState = "NC";
            this.BeneficiaryZipCode = "90210";
            this.BeneficiaryZipCode2 = string.Empty;
            this.HBIPayeeName = "Ronald McDonald" + mockId;
            this.HBIPayeeSSN = "3003" + mockId;

        }


        
    }//end class
}//end namespace