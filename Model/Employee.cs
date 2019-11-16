using System;
using System.Collections.Generic;

namespace NewPayDataTransformer.Model
{
    public partial class Employee
    {
        public Employee()
        {
            Employeeeft = new HashSet<Employeeeft>();
            Employeenoneft = new HashSet<Employeenoneft>();
        }

        public int Id { get; set; }
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
        public DateTime PayPeriodEndDate { get; set; }

        public virtual ICollection<Employeeeft> Employeeeft { get; set; }
        public virtual ICollection<Employeenoneft> Employeenoneft { get; set; }
    }
}
