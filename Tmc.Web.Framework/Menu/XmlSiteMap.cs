using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Xml;
using Tmc.Core.Common;
using Tmc.Core.Infrastructure;

namespace Tmc.Web.Framework.Menu
{
    public class XmlSiteMap
    {
        public SiteMapNode RootNode { get; set; }

        public XmlSiteMap()
        {
            RootNode = new SiteMapNode();
        }

        public virtual void LoadFrom(string virtualPath)
        {
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            var filePath = webHelper.MapPath(virtualPath);
            string content = File.ReadAllText(filePath);
            if (!string.IsNullOrEmpty(content))
            {
                using(var sr = new StringReader(content))
                {
                    using (var xmlReader = XmlReader.Create(sr,
                        new XmlReaderSettings 
                        { 
                            CloseInput = true,
                            IgnoreComments = true,
                            IgnoreWhitespace = true,
                            IgnoreProcessingInstructions = true
                        }))
                    {
                        var doc = new XmlDocument();
                        doc.Load(xmlReader);

                        if ((doc.DocumentElement != null) && doc.HasChildNodes)
                        {
                            XmlNode xmlRootNode = doc.DocumentElement.FirstChild;
                            Iterate(RootNode, xmlRootNode);
                        }
                    }
                }
            }
        }

        private static void Iterate(SiteMapNode siteMapNode, XmlNode xmlNode)
        {
            PopulateNode(siteMapNode, xmlNode);

            foreach (XmlNode xmlChildNode in xmlNode.ChildNodes)
            {
                if (xmlChildNode.LocalName.Equals("siteMapNode", StringComparison.InvariantCultureIgnoreCase))
                {
                    var siteMapChildNode = new SiteMapNode();
                    siteMapNode.ChildNodes.Add(siteMapChildNode);

                    Iterate(siteMapChildNode, xmlChildNode);
                }
            }
        }
        private static string GetStringValueFromAttribute(XmlNode node, string attributeName)
        {
            string value = null;

            if (node.Attributes.Count > 0)
            {
                XmlAttribute attribute = node.Attributes[attributeName];

                if (attribute != null)
                {
                    value = attribute.Value;
                }
            }

            return value;
        }

        private static void PopulateNode(SiteMapNode siteMapNode, XmlNode xmlNode)
        {
            //title
            //var nopResource = GetStringValueFromAttribute(xmlNode, "nopResource");
            //if (!string.IsNullOrEmpty(nopResource))
            //{
            //    var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            //    siteMapNode.Title = localizationService.GetResource(nopResource);
            //}
            //else
            {
                siteMapNode.Title = GetStringValueFromAttribute(xmlNode, "title");
            }

            //routes, url
            string controllerName = GetStringValueFromAttribute(xmlNode, "controller");
            string actionName = GetStringValueFromAttribute(xmlNode, "action");
            string url = GetStringValueFromAttribute(xmlNode, "url");
            if (!string.IsNullOrEmpty(controllerName) && !string.IsNullOrEmpty(actionName))
            {
                siteMapNode.ControllerName = controllerName;
                siteMapNode.ActionName = actionName;

                siteMapNode.RouteValues = new RouteValueDictionary()
                                          {
                                              {"area", "Admin"}
                                          };
            }
            else if (!string.IsNullOrEmpty(url))
            {
                siteMapNode.Url = url;
            }

            //image URL
            siteMapNode.ImageUrl = GetStringValueFromAttribute(xmlNode, "ImageUrl");

            siteMapNode.Visible = true;
            ////permission name
            //var permissionNames = GetStringValueFromAttribute(xmlNode, "PermissionNames");
            //if (!string.IsNullOrEmpty(permissionNames))
            //{
            //    var permissionService = EngineContext.Current.Resolve<IPermissionService>();
            //    siteMapNode.Visible = permissionNames.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            //       .Any(permissionName => permissionService.Authorize(permissionName.Trim()));
            //}
            //else
            //{
            //    siteMapNode.Visible = true;
            //}
        }
    }
}
