using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Domain.Customers;

namespace Tmc.Data.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        { 
            this.ToTable("Customer");
            this.HasKey(c => c.Id);
            this.Property(c => c.CustomerCode).IsRequired().HasMaxLength(10)
                .HasColumnAnnotation("Index",new IndexAnnotation(new IndexAttribute(){IsUnique = true}));
            this.Property(c => c.UserName).IsRequired().HasMaxLength(255);
            this.Ignore(c => c.CardTypeHistory);
            this.HasMany(c => c.CustomerRoles)
                .WithMany()
                .Map(m => m.ToTable("Customer_CustomerRole_Mapping"));
        }
    }
}
