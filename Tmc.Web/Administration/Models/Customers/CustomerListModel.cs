using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tmc.Web.Framework.Models;

namespace Tmc.Admin.Models.Customers
{
    public class CustomerListModel : BaseModel
    {
        [AllowHtml]
        public string SearchUserName { get; set; }
        public string SearchUserCode { get; set; }

    }
}