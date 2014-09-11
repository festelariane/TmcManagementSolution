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
        public string CustomerCode { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public decimal Points { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public DateTime LastActivityDateUtc { get; set; }
        public DateTime? LastLoginDateUtc { get; set; }
        private ICollection<CustomerRole> _customerRoles;
        public virtual ICollection<CustomerRole> CustomerRoles
        {
            get { return _customerRoles ?? (_customerRoles = new List<CustomerRole>()); }
            protected set { _customerRoles = value; }
        }

        private ICollection<CardType> _cardTypeHistory;
        public ICollection<CardType> CardTypeHistory
        {
            get { return _cardTypeHistory ?? (_cardTypeHistory = new List<CardType>()); }
            protected set { _cardTypeHistory = value; }
        }
    }
}
