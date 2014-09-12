using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.BLL.Contract.Customers;
using Tmc.BLL.Contract.Security;
using Tmc.Core;
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
        private readonly IRepository<CustomerRole> _customerRoleRepository;
        private readonly IEncryptionService _encryptionService;

        public CustomerBiz(IRepository<Customer> customerRepository, IRepository<CardType> cardTypeRepository,
            IRepository<CustomerRole> customerRoleRepository, IEncryptionService encryptionService)
        {
            this._customerRepository = customerRepository;
            this._cardTypeRepository = cardTypeRepository;
            this._customerRoleRepository = customerRoleRepository;
            this._encryptionService = encryptionService;
        }
        public IPagedList<Customer> GetAllCustomers(string userName, string userCode, int pageIndex = 0, int pageSize = 2147483647)
        {
            var query = _customerRepository.Table;
            if(!string.IsNullOrEmpty(userName))
            {
                query = query.Where(c => c.UserName.Contains(userName.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(userCode))
            {
                query = query.Where(c => c.CustomerCode.Contains(userCode.Trim()));
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

        private string BuildCustomerCode()
        {
            lock(typeof(CustomerBiz))
            {
                var latestId = _customerRepository.Table.LongCount();
                return (latestId + 1).ToString("D8");
            }
            
        }
        public Customer InsertCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            var existingCustomer = GetCustomerByUserName(customer.UserName);
            if(existingCustomer != null)
            {
                throw new ArgumentNullException("Customer with name: '{0}' exists", customer.UserName);
            }

            var cardType = _cardTypeRepository.GetById(customer.CardTypeId);
            if(cardType == null)
            {
                throw new ArgumentNullException("Please assign Card Type");
            }
            customer.CreatedOnUtc = DateTime.Now;
            customer.UpdatedOnUtc = customer.CreatedOnUtc;
            customer.LastActivityDateUtc = customer.CreatedOnUtc;
            customer.LastLoginDateUtc = null;
            customer.CustomerCode = BuildCustomerCode();

            var saltKey = _encryptionService.CreateSaltKey(5);

            customer.Password = _encryptionService.CreatePasswordHash(customer.CustomerCode,saltKey);
            customer.PasswordSalt = saltKey;

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

        #region Customer roles

        /// <summary>
        /// Delete a customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        public virtual void DeleteCustomerRole(CustomerRole customerRole)
        {
            if (customerRole == null)
                throw new ArgumentNullException("customerRole");

            if (customerRole.IsSystemRole)
                throw new TmcException("System role could not be deleted");

            _customerRoleRepository.Delete(customerRole);
        }

        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="customerRoleId">Customer role identifier</param>
        /// <returns>Customer role</returns>
        public virtual CustomerRole GetCustomerRoleById(int customerRoleId)
        {
            if (customerRoleId == 0)
                return null;

            return _customerRoleRepository.GetById(customerRoleId);
        }

        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="systemName">Customer role system name</param>
        /// <returns>Customer role</returns>
        public virtual CustomerRole GetCustomerRoleBySystemName(string systemName)
        {
            if (String.IsNullOrWhiteSpace(systemName))
                return null;

            var query = from cr in _customerRoleRepository.Table
                        orderby cr.Id
                        where cr.SystemName == systemName
                        select cr;
            var customerRole = query.FirstOrDefault();
            return customerRole;
        }

        /// <summary>
        /// Gets all customer roles
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Customer role collection</returns>
        public virtual IList<CustomerRole> GetAllCustomerRoles()
        {
            var query = from cr in _customerRoleRepository.Table
                        orderby cr.Name
                        //where (showHidden || cr.Active)
                        select cr;
            var customerRoles = query.ToList();
            return customerRoles;
        }

        /// <summary>
        /// Inserts a customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        public virtual void InsertCustomerRole(CustomerRole customerRole)
        {
            if (customerRole == null)
                throw new ArgumentNullException("customerRole");

            _customerRoleRepository.Insert(customerRole);

        }

        /// <summary>
        /// Updates the customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        public virtual void UpdateCustomerRole(CustomerRole customerRole)
        {
            if (customerRole == null)
                throw new ArgumentNullException("customerRole");

            _customerRoleRepository.Update(customerRole);

        }

        public bool AssignUserToRoles(int customerId, IList<int> roleIds)
        {
            var customer = GetCustomerById(customerId);
            if(customer == null)
            {
                throw new ArgumentNullException("customer");
            }
            if(roleIds == null)
            {
                return false;
            }
            var removedRoles = customer.CustomerRoles.Where(r => !roleIds.Contains(r.Id)).ToList();
            foreach(var role in removedRoles)
            {
                customer.CustomerRoles.Remove(role);
            }
            var currentRoleIds = customer.CustomerRoles.Select(r => r.Id).ToList();
            var newRoleIds = roleIds.Except(currentRoleIds).ToList();

            if (newRoleIds.Count > 0)
            {
                var roles = _customerRoleRepository.Table.Where(cr => newRoleIds.Contains(cr.Id));
                foreach (var role in roles)
                {
                    customer.CustomerRoles.Add(role);
                } 
            }
            UpdateCustomer(customer);
            return true;
        }
        #endregion
    }
}
