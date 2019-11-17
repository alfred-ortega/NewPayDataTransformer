using System;

namespace NewPayDataTransformer.Model
{
    public class MockEmployeeEft : BaseMockObject
    {
        public int Id { get; set; }
        public string RecipientName { get {return "Recipient " + LPad(Id.ToString(), 5); }  }
        public string RoutingNumber { get {return "1" + LPad(Id.ToString(), 8);} }
        public string AccountNumber { get {return "2" + LPad(Id.ToString(), 15);} }
        public DateTime PayPeriodEndDate { get; set; }

        public MockEmployeeEft(int id, DateTime payPeriodEndDate)
        {
            Id = id;
            PayPeriodEndDate = payPeriodEndDate;
        }

    }//end class
}//end namespace