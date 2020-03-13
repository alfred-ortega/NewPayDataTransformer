using System;

namespace NewPayDataTransformer.Model
{
    public partial class Employee : BaseMockObject, IEmployee, IComparable<Employee>
    {
        public string FullName {get {return string.Format("{0}, {1} {2}",this.LastName,this.FirstName, this.MiddleName ).Trim();}}

        public string FullZipCode { get {return string.Format("{0}-{1}",ZipCode,ZipCode2);}}

        public string MockSSN {get {return  "1001" + getMockId(this.Id);}}

        public MockEmployee GetMockedEmployee()
        {
            return new MockEmployee(this);
        }

        public int CompareTo(Employee other)
        {
            return this.Emplid.CompareTo(other.Emplid);
        }


        public Employee(string agency
                       ,string ssn
                       ,string lastName
                       ,string firstName
                       ,string middleName
                       ,DateTime dateOfBirth
                       )
        {
            this.Agency = agency;
            this.Emplid = ssn;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.DateOfBirth = dateOfBirth;
      
        }


        private string getMockId(int id)
        {
            return LPad(id.ToString(),5);
        }        
    }//end class
}//end namespace