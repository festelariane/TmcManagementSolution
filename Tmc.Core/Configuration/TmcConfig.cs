using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Xml;

namespace Tmc.Core.Configuration
{
    public partial class TmcConfig : ConfigurationSection
    {
        /// <summary>
        /// In addition to configured assemblies examine and load assemblies in the bin directory.
        /// </summary>
        public bool DynamicDiscovery 
        {
            get
            {
                return true;
            }
            //get
            //{
            //    return (bool)this["DynamicDiscovery"];
            //}
            //private set
            //{
            //    this["DynamicDiscovery"] = value;
            //}
        }
        /// <summary>
        /// A custom <see cref="IEngine"/> to manage the application instead of the default.
        /// </summary>
        public string EngineType 
        {
            get
            {
                return "";
            }
            //get
            //{
            //    return (string)this["EngineType"];
            //}
            //private set
            //{
            //    this["EngineType"] = value;
            //}
        }
        /// <summary>
        /// Specifices where the themes will be stored (~/Themes/)
        /// </summary>
        public string ThemeBasePath 
        {
            get
            {
                return "~/Themes/";
            }
            //get
            //{
            //    return (string)this["ThemeBasePath"];
            //}
            //private set
            //{
            //    this["ThemeBasePath"] = value;
            //}
        }
        /// <summary>
        /// Indicates whether we should ignore startup tasks
        /// </summary>
        public bool IgnoreStartupTasks 
        {
            get
            {
                return true;
            }
            //get
            //{
            //    return (bool)this["IgnoreStartupTasks"];
            //}
            //private set
            //{
            //    this["IgnoreStartupTasks"] = value;
            //}    
        }
    }
}
