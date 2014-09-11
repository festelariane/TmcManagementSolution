using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Domain.Transaction;

namespace Tmc.Data.Mapping
{
    public class WithdrawTransactionMap : EntityTypeConfiguration<WithdrawTransaction>
    {
        public WithdrawTransactionMap()
        {
            this.ToTable("WithdrawTransaction");
            this.HasKey(c => c.Id);
            this.Property(c => c.CreatedOnUtc).IsRequired();
        }
    }
}
