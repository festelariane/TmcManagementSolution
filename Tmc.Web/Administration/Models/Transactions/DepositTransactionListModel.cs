using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tmc.Web.Framework.Models;

namespace Tmc.Admin.Models.Transactions
{
    public class DepositTransactionListModel : BaseModel
    {
        [AllowHtml]
        public int? customerId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string UserName { get; set; }
    }
}