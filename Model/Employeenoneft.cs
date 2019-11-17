using System;
using System.Collections.Generic;

namespace NewPayDataTransformer.Model
{
    public partial class Employeenoneft
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string RecipientName { get; set; }
        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ZipCode2 { get; set; }
        public string HomePhone { get; set; }
        public DateTime PayPeriodEndDate { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
