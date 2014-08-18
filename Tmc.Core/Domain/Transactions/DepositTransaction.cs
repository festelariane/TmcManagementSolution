using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Domain.Customers;

namespace Tmc.Core.Domain.Transaction
{
    public partial class DepositTransaction : BaseEntity
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public decimal Points { get; set; }
        public double ExchangeRate { get; set; }
    }
}
