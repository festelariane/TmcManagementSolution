using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tmc.Web.Framework.Models;

namespace Tmc.Web.Models.Common
{
    public class HeaderLinksModel : BaseModel
    {
        public bool IsAuthenticated { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public bool IsAdmin { get; set; }
    }
}