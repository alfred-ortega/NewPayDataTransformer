using System;

namespace NewPayDataTransformer.Model
{
    public class MockEmployee : IEmployee
    {
        public string Agency {get;}
        public string Ssn { get; }
        public string LastName { get; }
        public string FirstName { get; }
        public string MiddleName { get; }
        public string Suffix { get; }
        public DateTime DateOfBirth { get; private set;}
        public string StreetAddress { get; }
        public string StreetAddress2 { get; }
        public string StreetAddress3 { get; }
       
        public string City { get; }
        public string State { get; }
        public string ZipCode { get; }
        public string ZipCode2 { get; }
        public DateTime PayPeriodEndDate {get;} 
        public string FullName {get {return string.Format("{0}, {1} {2}",this.LastName,this.FirstName, this.MiddleName ).Trim();}}

        public string FullZipCode { get {return string.Format("{0}-{1}",ZipCode,ZipCode2);}}


        public MockEmployee(int id, DateTime payPeriodEndDate, string agency, DateTime dateOfBirth)
        {
            string mockId = getMockId(id);
            this.Agency = agency;
            this.PayPeriodEndDate = payPeriodEndDate;
            this.Ssn = "0000" + mockId;
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

        private string getMockId(int id)
        {
            string retval = id.ToString();
            while(retval.Length < 5)
            {
                retval = "0" + retval;
            }
            return retval;
        }

        
    }//end class
}//end namespace