using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Domain.Cards;

namespace Tmc.Core.Domain.Customers
{
    public class CustomerCardType : BaseEntity
    {
        public int CardTypeId { get; set; }
        public virtual CardType CardType
        {
            get;
            set;
        }

        public int CustomerId { get; set; }
        public virtual Customer Customer
        {
            get;
            set;
        }
        public DateTime StartDateUtc { get; set; }
        public DateTime EndDateUtc { get; set; }
    }
}
