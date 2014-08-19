using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tmc.Web.Framework.Models;

namespace Tmc.Admin.Models
{
    public class CardTypeListModel : BaseModel
    {
        [AllowHtml]
        public string SearchCardTypeName { get; set; }
    }
}