using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmc.Web.Framework.Themes
{
    public interface IThemeContext
    {
        /// <summary>
        /// Get or set current theme for desktops
        /// </summary>
        string WorkingDesktopTheme { get; set; }

        /// <summary>
        /// Get current theme for mobile 
        /// </summary>
        string WorkingMobileTheme { get; }
    }
}
