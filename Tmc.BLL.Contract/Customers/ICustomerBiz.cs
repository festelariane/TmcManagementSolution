using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Common;
using Tmc.Core.Domain.Customers;

namespace Tmc.BLL.Contract.Customers
{
    public partial interface ICustomerBiz
    {
        IPagedList<Customer> GetAllCustomers(string userName, string userCode, int pageIndex = 0, int pageSize = 2147483647);
        Customer GetCustomerById(int customerId);
        void DeleteCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        Customer InsertCustomer(Customer customer);
        Customer GetCustomerByUserName(string username);

        #region Customer roles

        /// <summary>
        /// Delete a customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        void DeleteCustomerRole(CustomerRole customerRole);

        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="customerRoleId">Customer role identifier</param>
        /// <returns>Customer role</returns>
        CustomerRole GetCustomerRoleById(int customerRoleId);

        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="systemName">Customer role system name</param>
        /// <returns>Customer role</returns>
        CustomerRole GetCustomerRoleBySystemName(string systemName);

        /// <summary>
        /// Gets all customer roles
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Customer role collection</returns>
        IList<CustomerRole> GetAllCustomerRoles();

        /// <summary>
        /// Inserts a customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        void InsertCustomerRole(CustomerRole customerRole);

        /// <summary>
        /// Updates the customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        void UpdateCustomerRole(CustomerRole customerRole);

        bool AssignUserToRoles(int customerId, IList<int> roleIds);
        #endregion
    }
}
