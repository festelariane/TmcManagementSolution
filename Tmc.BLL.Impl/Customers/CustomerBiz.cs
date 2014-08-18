using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.BLL.Contract.Customers;
using Tmc.Core.Common;
using Tmc.Core.Data;
using Tmc.Core.Domain.Customers;

namespace Tmc.BLL.Impl.Customers
{
    public class CustomerBiz : ICustomerBiz
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerBiz(IRepository<Customer> customerRepository)
        {
            this._customerRepository = customerRepository;
        }
        public IPagedList<Customer> GetAllCustomers(int pageIndex = 0, int pageSize = 2147483647)
        {
            var query = _customerRepository.Table.OrderBy(c => c.UserName);

            return new PagedList<Customer>(query, pageIndex, pageSize); ;
        }

        public Customer GetCustomerById(int customerId)
        {
            if(customerId <= 0)
            {
                return null;
            }
            return _customerRepository.GetById(customerId);
        }

        public void DeleteCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            _customerRepository.Delete(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            _customerRepository.Update(customer);
        }

        public Customer InsertCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            _customerRepository.Insert(customer);
            return customer;
        }
    }
}
