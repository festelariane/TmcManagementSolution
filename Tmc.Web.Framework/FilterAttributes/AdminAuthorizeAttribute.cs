using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tmc.Core.Common;
using Tmc.Core.Domain.Customers;
using Tmc.Core.Infrastructure;
using Tmc.Core.Domain.Extensions;

namespace Tmc.Web.Framework.FilterAttributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AdminAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        private string _RoleNames = "";
        
        public AdminAuthorizeAttribute(string RoleNames = "")
        {
            this._RoleNames = RoleNames;
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");
            if(string.IsNullOrWhiteSpace(_RoleNames))
            {
                _RoleNames = SystemRoleName.Administrators;
            }
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            var currentUser = workContext.CurrentCustomer;

            var bAuthorized = false;
            if(currentUser == null)
            {
                this.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                
                string[] roles = _RoleNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if(roles != null && roles.Length > 0)
                {
                    foreach(var role in roles)
                    {
                        if(currentUser.IsInCustomerRole(role))
                        {
                            bAuthorized = true;
                            break;
                        }
                    }
                }
            }
            if(!bAuthorized)
            {
                this.HandleUnauthorizedRequest(filterContext);
            }
        }

        private void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}
