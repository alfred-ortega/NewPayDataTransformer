using System;

namespace NewPayDataTransformer.Model
{
    public class MockEmployeeNonEft : BaseMockObject
    {
        public int Id { get; private set; }
        public string RecipientName { get {return "ACME Inc File #" + LPad(Id.ToString(), 4);}  }
        public string StreetAddress { get {return LPad(Id.ToString(), 5) + " 3rd Avenue"; }  }
        public string StreetAddress2 { get {return string.Empty;}  }
        public string City { get {return "LEES SUMMIT";}  }
        public string State { get {return "MO";}  }
        public string ZipCode { get {return "64082";}  }
        public string ZipCode2 { get {return string.Empty;}  }
        public string HomePhone { get {return "8161" + LPad(Id.ToString(),6);}  }
        public DateTime PayPeriodEndDate { get; private set; }

        public string MockSsn {get;set;}

        public MockEmployeeNonEft(int id, DateTime payPeriodEndDate, string mockSsn)
        {
            Id = id;
            PayPeriodEndDate = payPeriodEndDate;
            MockSsn = mockSsn;
        }

    }//end class
}//end namespace