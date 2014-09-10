using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tmc.Web.Framework.Common;
using Tmc.Web.Framework.FilterAttributes;

namespace Tmc.Admin.Controllers
{
    public class DownloadController : BaseAdminController
    {
        [AdminAuthorize]
        public ActionResult DownloadExcel(string key)
        {
            if(string.IsNullOrWhiteSpace(key))
            {
                return new NullJsonResult();
            }

            if (Session[key] != null && Session[key] is byte[])
            {
                return File((byte[])Session[key], "text/xls", "transactions.xlsx");
            }
            return null;
        }
    }
}