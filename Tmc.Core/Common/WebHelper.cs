using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Tmc.Core.Common
{
    public class WebHelper : IWebHelper
    {
        /// <summary>
        /// Maps a virtual path to a physical disk path.
        /// </summary>
        public string MapPath(string path)
        {
            if(HostingEnvironment.IsHosted)
            {
                return HostingEnvironment.MapPath(path);
            }
            else
            {
                //not hosted. For example, run in unit tests
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
                return Path.Combine(baseDirectory, path);
            }
        }
    }
}
