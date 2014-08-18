using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Core.Domain.Cards
{
    public partial class CardType: BaseEntity
    {
        public string Name { get; set; }
        public double ExchangeRate { get; set; }
        public decimal Threshold { get; set; }
        public int DisplayOrder { get; set; }
    }
}
