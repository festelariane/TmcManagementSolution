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
        IPagedList<Customer> GetAllCustomers(string userName, string FullName, int pageIndex = 0, int pageSize = 2147483647);
        Customer GetCustomerById(int customerId);
        void DeleteCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        Customer InsertCustomer(Customer customer);
        Customer GetCustomerByUserName(string username);
    }
}
