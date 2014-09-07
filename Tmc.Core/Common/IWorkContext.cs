using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Domain.Customers;

namespace Tmc.Core.Common
{
    public interface IWorkContext
    {
        Customer CurrentCustomer { get; set; }
        bool IsAdmin { get;}
    }
}
