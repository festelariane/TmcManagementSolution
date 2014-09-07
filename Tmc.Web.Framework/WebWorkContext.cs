using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.BLL.Contract.Authentication;
using Tmc.Core.Common;
using Tmc.Core.Domain.Customers;
using Tmc.Core.Domain.Extensions;

namespace Tmc.Web.Framework
{
    public class WebWorkContext : IWorkContext
    {
        private readonly IAuthenticationBiz _authenticationBiz;
        private Customer _cachedCustomer;

        public WebWorkContext(IAuthenticationBiz authenticationBiz)
        {
            this._authenticationBiz = authenticationBiz;
        }
        public virtual Customer CurrentCustomer
        {
            get
            {
                if (_cachedCustomer != null)
                    return _cachedCustomer;
                var customer = _authenticationBiz.GetAuthenticatedCustomer();
                _cachedCustomer = customer;

                return _cachedCustomer;
            }
            set
            {
                _cachedCustomer = value;
            }
        }

        public bool IsAdmin
        {
            get
            {
                if(CurrentCustomer == null)
                {
                    return false;
                }
                return CurrentCustomer.IsInCustomerRole(SystemRoleName.Administrators);
            }
            private set
            {
                //Do nothing here
            }
        }
    }
}
