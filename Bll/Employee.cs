using System;

namespace NewPayDataTransformer.Model
{
    public partial class Employee : IEmployee, IComparable<Employee>
    {
        public string FullName {get {return string.Format("{0}, {1} {2}",this.LastName,this.FirstName, this.MiddleName ).Trim();}}

        public string FullZipCode { get {return string.Format("{0}-{1}",ZipCode,ZipCode2);}}

        public string MockSSN {get {return  "1001" + getMockId(this.Id);}}

        public MockEmployee GetMockedEmployee()
        {
            return new MockEmployee(getMockId(this.Id), this.PayPeriodEndDate,this.Agency, this.DateOfBirth);
        }

        public int CompareTo(Employee other)
        {
            return this.Ssn.CompareTo(other.Ssn);
        }


        public Employee(string agency
                       ,string ssn
                       ,string lastName
                       ,string firstName
                       ,string middleName
                       ,string suffix
                       ,DateTime dateOfBirth
                       ,string streetAddress
                       ,string streetAddress2
                       ,string streetAddress3
                       ,string city
                       ,string state
                       ,string zipCode
                       ,string zipCode2
                       ,DateTime payPeriodEndDate
                       )
        {
            this.Agency = agency;
            this.Ssn = ssn;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.Suffix = suffix;
            this.DateOfBirth = dateOfBirth;
            this.StreetAddress = streetAddress;
            this.StreetAddress3 = streetAddress3;
            this.City = city;
            this.State = state;
            this.ZipCode = zipCode;
            this.ZipCode2 = zipCode2;
            this.PayPeriodEndDate = payPeriodEndDate;            
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