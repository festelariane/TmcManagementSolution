using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Domain.Customers;

namespace Tmc.Data.Mapping
{
    public class CustomerCardTypeMap : EntityTypeConfiguration<CustomerCardType>
    {
        public CustomerCardTypeMap()
        {
            this.ToTable("Customer_CardType_Mapping");
            this.HasKey(cct => cct.Id);
            this.HasRequired(cct => cct.Customer)
                .WithMany()
                .HasForeignKey(cct => cct.CustomerId)
                .WillCascadeOnDelete(false);
            this.HasRequired(cct => cct.CardType)
                .WithMany()
                .HasForeignKey(cct => cct.CardTypeId)
                .WillCascadeOnDelete(false);
        }
    }
}
