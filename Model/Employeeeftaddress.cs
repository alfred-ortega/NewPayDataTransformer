using System;
using System.Collections.Generic;

namespace NewPayDataTransformer.Model
{
    public partial class Employeeeftaddress
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string BankStreetAddress { get; set; }
        public string BankStreetAddress2 { get; set; }
        public DateTime PayPeriodEndDate { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
