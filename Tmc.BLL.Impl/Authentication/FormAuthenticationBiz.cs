using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Tmc.BLL.Contract.Authentication;
using Tmc.BLL.Contract.Customers;
using Tmc.Core.Domain.Customers;

namespace Tmc.BLL.Impl.Authentication
{
    public class FormAuthenticationBiz : IAuthenticationBiz
    {
        private readonly HttpContextBase _httpContext;
        private readonly ICustomerBiz _customerBiz;
        private readonly TimeSpan _expirationTimeSpan;
        private Customer _cachedCustomer;
        public FormAuthenticationBiz(HttpContextBase httpContext, ICustomerBiz customerBiz)
        {
            this._httpContext = httpContext;
            this._customerBiz = customerBiz;
            _expirationTimeSpan = FormsAuthentication.Timeout;
        }

        public virtual void SignIn(Customer customer, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();
            var ticket = new FormsAuthenticationTicket(1, customer.UserName, now, now.Add(_expirationTimeSpan), createPersistentCookie, customer.UserName, FormsAuthentication.FormsCookiePath);
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if(FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }
            _httpContext.Response.Cookies.Add(cookie);
            _cachedCustomer = customer;
        }

        public virtual void SignOut()
        {
            _cachedCustomer = null;
            FormsAuthentication.SignOut();
        }

        public virtual Customer GetAuthenticatedCustomer()
        {
            if(_cachedCustomer != null)
            {
                return _cachedCustomer;
            }
            if (_httpContext == null ||
                _httpContext.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }
            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var customer = GetAuthenticatedCustomerFromTicket(formsIdentity.Ticket);
            _cachedCustomer = customer;
            return _cachedCustomer;
        }
        public virtual Customer GetAuthenticatedCustomerFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var userName = ticket.UserData;

            if (String.IsNullOrWhiteSpace(userName))
                return null;
            var customer = _customerBiz.GetCustomerByUserName(userName);
            return customer;
        }
    }
}
