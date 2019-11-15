using System;

namespace NewPayDataTransformer.Model
{
    public class TempEmployee : IEmployee
    {        
        public string Agency { get; set; }
        public string Ssn { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Suffix { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }
        public string StreetAddress3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ZipCode2 { get; set; }
        public DateTime PayPeriodEndDate {get;set;} 
       public string FullName {get {return string.Format("{0}, {1} {2}",this.LastName,this.FirstName, this.MiddleName ).Trim();}}        

       public Employee CreateEmployee()
       {
           Employee emp = new Employee(this.Agency
                                      ,this.Ssn
                                      ,this.LastName
                                      ,this.FirstName
                                      ,this.MiddleName
                                      ,this.Suffix
                                      ,this.DateOfBirth
                                      ,this.StreetAddress
                                      ,this.StreetAddress2
                                      ,this.StreetAddress3
                                      ,this.City
                                      ,this.State
                                      ,this.ZipCode
                                      ,this.ZipCode2
                                      ,this.PayPeriodEndDate
                                      );
           return emp;
       }
        
    }//end class
}//end namespace