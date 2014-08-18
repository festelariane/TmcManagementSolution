using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Domain.Cards;

namespace Tmc.Core.Domain.Customers
{
    public partial class Customer : BaseEntity
    {
        public int CardTypeId { get; set; }
        public virtual CardType CardType
        {
            get;
            set;
        }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public decimal Points { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public DateTime LastActivityDateUtc { get; set; }
    }
}
