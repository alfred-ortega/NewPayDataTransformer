using System;
using System.Collections.Generic;

namespace NewPayDataTransformer.Model
{
    public partial class Employee
    {
        public long Id { get; set; }
        public string Emplid { get; set; }
        public string Agency { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string DateOfBirth { get; set; }
        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Ssn { get; set; }
    }
}
