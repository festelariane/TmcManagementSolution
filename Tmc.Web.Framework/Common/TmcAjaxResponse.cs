using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Web.Framework.Common
{
    public class TmcAjaxResponse
    {
        public TmcAjaxResponse()
        {
            Success = true;
            Errors = null;
            Data = null;
        }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public object Data { get; set; }
    }
}
