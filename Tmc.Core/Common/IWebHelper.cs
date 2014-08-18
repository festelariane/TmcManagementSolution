using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Core.Common
{
    public interface IWebHelper
    {
        /// <summary>
        /// Maps a virtual path to a physical path.
        /// </summary>
        string MapPath(string path);
    }
}
