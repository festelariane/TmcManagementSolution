using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Tmc.Web.Framework.Common
{
    public class JsonStringResult : ContentResult
    {
        public JsonStringResult(string sReturnString)
        {
            Content = sReturnString;
            ContentType = "application/json";
        }
    }
}
