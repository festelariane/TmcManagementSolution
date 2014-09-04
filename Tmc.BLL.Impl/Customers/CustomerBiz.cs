using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.BLL.Contract.Customers;
using Tmc.Core.Common;
using Tmc.Core.Data;
using Tmc.Core.Domain.Cards;
using Tmc.Core.Domain.Customers;

namespace Tmc.BLL.Impl.Customers
{
    public class CustomerBiz : ICustomerBiz
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CardType> _cardTypeRepository;

        public CustomerBiz(IRepository<Customer> customerRepository, IRepository<CardType> cardTypeRepository)
        {
            this._customerRepository = customerRepository;
            this._cardTypeRepository = cardTypeRepository;
        }
        public IPagedList<Customer> GetAllCustomers(string userName, string fullName, int pageIndex = 0, int pageSize = 2147483647)
        {
            var query = _customerRepository.Table;
            if(!string.IsNullOrEmpty(userName))
            {
                query = query.Where(c => c.UserName.Contains(userName.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                query = query.Where(c => c.UserName.Contains(fullName.Trim()));
            }
            query = query.OrderBy(c => c.UserName);
            return new PagedList<Customer>(query, pageIndex, pageSize);
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
            var cardType = _cardTypeRepository.GetById(customer.CardTypeId);
            if (cardType == null)
            {
                throw new ArgumentNullException("Please assign Card Type");
            }
            _customerRepository.Update(customer);
        }

        public Customer InsertCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            var cardType = _cardTypeRepository.GetById(customer.CardTypeId);
            if(cardType == null)
            {
                throw new ArgumentNullException("Please assign Card Type");
            }
            customer.CreatedOnUtc = DateTime.Now;
            customer.UpdatedOnUtc = customer.CreatedOnUtc;
            customer.LastActivityDateUtc = customer.CreatedOnUtc;

            _customerRepository.Insert(customer);
            return customer;
        }

        public virtual Customer GetCustomerByUserName(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            var query = from c in _customerRepository.Table
                        orderby c.Id
                        where c.UserName == username
                        select c;
            var customer = query.FirstOrDefault();
            return customer;
        }
    }
}
