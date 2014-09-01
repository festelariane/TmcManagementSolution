﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.BLL.Contract.Transactions;
using Tmc.Core.Common;
using Tmc.Core.Data;
using Tmc.Core.Domain.Customers;
using Tmc.Core.Domain.Transaction;

namespace Tmc.BLL.Impl.Transactions
{
    public class DepositTransactionBiz : IDepositTransactionBiz
    {
        private readonly IRepository<DepositTransaction> _depositTranRepository;
        private readonly IRepository<Customer> _customerRepository;

        public DepositTransactionBiz(IRepository<DepositTransaction> depositTranRepository, IRepository<Customer> customerRepository)
        {
            this._depositTranRepository = depositTranRepository;
            this._customerRepository = customerRepository;
        }

        public IPagedList<DepositTransaction> GetAllDepositTransactions(int? customerId, DateTime? dateFromUtc, DateTime? dateToUtc, int pageIndex = 0, int pageSize = 2147483647)
        {
            var query = _depositTranRepository.Table;
            if (customerId.GetValueOrDefault(0) > 0)
            {
                query = query.Where(c => c.CustomerId == customerId.Value);
            }
            
            query = query.OrderByDescending(dt => dt.CreatedOnUtc);
            return new PagedList<DepositTransaction>(query, pageIndex, pageSize);
        }

        public DepositTransaction GetDepositTransactionById(int depTranId)
        {
            if (depTranId <= 0)
            {
                return null;
            }
            return _depositTranRepository.GetById(depTranId);
        }

        public void DeleteDepositTransaction(DepositTransaction depTran)
        {
            throw new NotImplementedException();
        }

        public void UpdateDepositTransaction(DepositTransaction depTran)
        {
            throw new NotImplementedException();
        }

        public DepositTransaction InsertDepositTransaction(DepositTransaction depTran)
        {
            if (depTran == null)
                throw new ArgumentNullException("depositTransaction");

            var customer = _customerRepository.GetById(depTran.CustomerId);
            if (customer == null)
            {
                throw new ArgumentNullException("Cannot find specified customer");
            }
            depTran.CreatedOnUtc = DateTime.Now;

            _depositTranRepository.Insert(depTran);
            return depTran;
        }

        public bool Deposit(int customerId, decimal depositAmount)
        {
            var customer = _customerRepository.GetById(customerId);
            if (customer == null)
            {
                throw new ArgumentNullException("Cannot find specified customer");
            }
            if(customer.CardType == null)
            {
                throw new ArgumentNullException("This customer doesn't have a valid card type");
            }
            var points = (decimal)(customer.CardType.ExchangeRate* (double)depositAmount);
            var depositTran = new DepositTransaction();
            depositTran.Customer = customer;
            depositTran.Amount = depositAmount;
            depositTran.Points = points;
            depositTran.CreatedOnUtc = DateTime.Now;
            depositTran.ExchangeRate = customer.CardType.ExchangeRate;

            _depositTranRepository.Insert(depositTran);

            customer.Points = customer.Points + points;
            _customerRepository.Update(customer);
            return true;
        }
    }
}