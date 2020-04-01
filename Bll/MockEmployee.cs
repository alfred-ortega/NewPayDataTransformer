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
        public string DateOfBirth { get; private set;}
        public string StreetAddress {get; private set;}
        public string StreetAddress2 {get; private set;}
       
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Ssn { get; set; }
        public string County { get; set; }



        public string FullName {get {return string.Format("{0}, {1} {2}",this.LastName,this.FirstName, this.MiddleName ).Trim();}}



        public MockEmployee(Employee employee)
        {
            setInitValues( this.LPad(employee.Id.ToString(),5), employee.Agency, employee.DateOfBirth);
        }

        public MockEmployee(string ssn)
        {
            this.Emplid = ssn;
            
        }


        public void setInitValues(string mockId, string agency, string dateOfBirth)
        {
            this.Agency = agency;
            this.Emplid = mockId;
            this.Ssn = "1001" + mockId;
            this.DateOfBirth = dateOfBirth; 
            this.LastName = "BAINES" + mockId;
            this.FirstName = "ROBERT";
            this.MiddleName = string.Empty;
            this.StreetAddress = string.Format("1{0} Fake Street Address Here",mockId);
            this.StreetAddress2 = string.Empty;
            this.City = "KANSAS CITY";
            this.State = "MO";
            this.ZipCode = "64108";

        }


        
    }//end class
}//end namespace