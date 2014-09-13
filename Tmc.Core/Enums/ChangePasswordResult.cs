using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Core.Enums
{
    public enum ChangePasswordResult : int
    {
        Successful = 1,

        CustomerNotExist = 2,

        OldPasswordNotValid = 3,
      
        
        CannotChange = 4
    }
}
