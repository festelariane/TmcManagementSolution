using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tmc.Web.Framework.Models;

namespace Tmc.Web.Models.Customers
{
    public class CustomerInfoModel : BaseEntityModel
    {
        public string CustomerCode { get; set; }
        public string CardType { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public decimal Points { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public DateTime LastActivityDateUtc { get; set; }
    }
}