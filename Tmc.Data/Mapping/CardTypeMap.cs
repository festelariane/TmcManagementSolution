using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Domain.Cards;

namespace Tmc.Data.Mapping
{
    public class CardTypeMap : EntityTypeConfiguration<CardType>
    {
        public CardTypeMap()
        {
            this.ToTable("CardType");
            this.HasKey(ct => ct.Id);
            this.Property(ct => ct.Name).IsRequired().HasMaxLength(255);
            this.Property(ct => ct.ExchangeRate).IsRequired();
            this.Property(ct => ct.Threshold).IsRequired();
            this.Property(ct => ct.Prefix).HasMaxLength(5);
        }
    }
}
