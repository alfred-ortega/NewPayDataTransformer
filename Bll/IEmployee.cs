using System;

namespace NewPayDataTransformer.Model
{
     interface IEmployee
    {
         string Agency {get;}
         string Emplid {get;}
         string LastName { get;  }
         string FirstName { get;  }
         string MiddleName { get;  }
         DateTime DateOfBirth { get;  }
         string FullName {get;}  

    }
}