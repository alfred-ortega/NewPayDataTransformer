using System;

namespace NewPayDataTransformer.Model
{
    public class TempEmployee : IEmployee
    {        
        public string Agency { get; set; }
        public string Emplid { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }

       public string FullName {get {return string.Format("{0}, {1} {2}",this.LastName,this.FirstName, this.MiddleName ).Trim();}}        

       public Employee CreateEmployee()
       {
           Employee emp = new Employee(this.Agency
                                      ,this.Emplid
                                      ,this.LastName
                                      ,this.FirstName
                                      ,this.MiddleName
                                      ,this.DateOfBirth
                                      );
           return emp;
       }
        
    }//end class
}//end namespace