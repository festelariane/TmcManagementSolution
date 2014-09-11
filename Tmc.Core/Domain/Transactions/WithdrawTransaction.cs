using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Domain.Customers;

namespace Tmc.Core.Domain.Transaction
{
    public partial class WithdrawTransaction : BaseEntity
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public decimal Points { get; set; }
        public string Reason { get; set; }
    }
}
