using System;

namespace NewPayDataTransformer.Model
{
    public class MockEmployeeEftAddress :BaseMockObject
    {
        public int Id { get; private set; }
        public string BankName { get {return  "Bank of Oz (Topeka)";} }
        public string AccountNumber { get{return LPad(Id.ToString(),12);}  }
        public string BankStreetAddress { get {return "1313 Yellow Brick Road"; }}
        public string BankStreetAddress2 { get{return "Topeka, Oz 12345";}  }
        public DateTime PayPeriodEndDate { get;  }
        public string MockSsn {get; private set;}
        public MockEmployee Employee {get;}


        public MockEmployeeEftAddress(int id, DateTime payPeriodEndDate, MockEmployee mockEmployee)
        {
            Employee = mockEmployee;
            PayPeriodEndDate = payPeriodEndDate;
            Id = id;
        }


    }//end class
}//end namespace