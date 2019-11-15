using System;

namespace NewPayDataTransformer.Model
{
     interface IEmployee
    {
         string Agency {get;}
         string Ssn {get;}
         string LastName { get;  }
         string FirstName { get;  }
         string MiddleName { get;  }
         string Suffix { get;  }
         DateTime DateOfBirth { get;  }
         string StreetAddress { get;  }
         string StreetAddress2 { get;  }
         string StreetAddress3 { get;  }
         string City { get;  }
         string State { get;  }
         string ZipCode { get;  }
         string ZipCode2 { get;  }
         string FullName {get;}  

         DateTime PayPeriodEndDate {get;}       
    }
}