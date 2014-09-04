using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Core.Enums
{
    public enum LoginResult : int
    {
        Successful = 1,
        
        CustomerNotExist = 2,
      
        WrongPassword = 3,
     
        NotActive = 4,
        
        Deleted = 5,

        NotRegistered = 6,
    }
}
