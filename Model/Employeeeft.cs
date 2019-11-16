using System;
using System.Collections.Generic;

namespace NewPayDataTransformer.Model
{
    public partial class Employeeeft
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string RecipientName { get; set; }
        public string RoutingNumber { get; set; }
        public string AccountNumber { get; set; }
        public DateTime? PayPeriodEndDate { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
