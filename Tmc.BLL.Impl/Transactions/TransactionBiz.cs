using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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
    public class TransactionBiz : ITransactionBiz
    {
        private readonly IRepository<WithdrawTransaction> _withdrawTranRepository;
        private readonly IRepository<DepositTransaction> _depositTranRepository;
        private readonly IRepository<Customer> _customerRepository;

        public TransactionBiz(IRepository<DepositTransaction> depositTranRepository, IRepository<WithdrawTransaction> withdrawTranRepository, IRepository<Customer> customerRepository)
        {
            this._depositTranRepository = depositTranRepository;
            this._withdrawTranRepository = withdrawTranRepository;
            this._customerRepository = customerRepository;
        }

        public IPagedList<DepositTransaction> GetAllDepositTransactions(int? customerId, string CustomerUserName, DateTime? dateFromUtc, DateTime? dateToUtc, int pageIndex = 0, int pageSize = 2147483647)
        {
            var query = _depositTranRepository.Table;
            if (customerId.GetValueOrDefault(0) > 0)
            {
                query = query.Where(c => c.CustomerId == customerId.Value);
            }
            else
            {
                if(!string.IsNullOrWhiteSpace(CustomerUserName))
                {
                    query = query.Join(_customerRepository.Table.Where(c => c.UserName.Contains(CustomerUserName)), dt => dt.CustomerId, c => c.Id, (dt, c) => dt);
                
                }
            }
            if(dateFromUtc.HasValue)
            {
                var fromDate = dateFromUtc.Value.Date;
                //For better performance, shouldn't use TruncateTime.
                //query = query.Where(dt => DbFunctions.TruncateTime(dt.CreatedOnUtc) >= dateFromUtc.Value.Date);
                query = query.Where(dt => dt.CreatedOnUtc >= fromDate);
            }
            if (dateToUtc.HasValue)
            {
                var dateTo = dateToUtc.Value.Date.AddDays(1);
                query = query.Where(dt => dt.CreatedOnUtc < dateTo);
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

        public IPagedList<WithdrawTransaction> GetAllWithdrawTransactions(int? customerId, string CustomerUserName, DateTime? dateFromUtc, DateTime? dateToUtc, int pageIndex = 0, int pageSize = 2147483647)
        {
            var query = _withdrawTranRepository.Table;
            if (customerId.GetValueOrDefault(0) > 0)
            {
                query = query.Where(c => c.CustomerId == customerId.Value);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(CustomerUserName))
                {
                    query = query.Join(_customerRepository.Table.Where(c => c.UserName.Contains(CustomerUserName)), dt => dt.CustomerId, c => c.Id, (dt, c) => dt);

                }
            }
            if (dateFromUtc.HasValue)
            {
                var fromDate = dateFromUtc.Value.Date;
                //For better performance, shouldn't use TruncateTime.
                //query = query.Where(dt => DbFunctions.TruncateTime(dt.CreatedOnUtc) >= dateFromUtc.Value.Date);
                query = query.Where(dt => dt.CreatedOnUtc >= fromDate);
            }
            if (dateToUtc.HasValue)
            {
                var dateTo = dateToUtc.Value.Date.AddDays(1);
                query = query.Where(dt => dt.CreatedOnUtc < dateTo);
            }
            query = query.OrderByDescending(dt => dt.CreatedOnUtc);
            return new PagedList<WithdrawTransaction>(query, pageIndex, pageSize);
        }

        public WithdrawTransaction GetWithdrawTransactionById(int withTranId)
        {
            if (withTranId <= 0)
            {
                return null;
            }
            return _withdrawTranRepository.GetById(withTranId);
        }

        public void DeleteWithdrawTransaction(WithdrawTransaction withTran)
        {
            throw new NotImplementedException();
        }

        public void UpdateWithdrawTransaction(WithdrawTransaction depTran)
        {
            throw new NotImplementedException();
        }

        public WithdrawTransaction InsertWithdrawTransaction(WithdrawTransaction withTran)
        {
            if (withTran == null)
                throw new ArgumentNullException("WithdrawTransaction");

            var customer = _customerRepository.GetById(withTran.CustomerId);
            if (customer == null)
            {
                throw new ArgumentNullException("Cannot find specified customer");
            }
            withTran.CreatedOnUtc = DateTime.Now;

            _withdrawTranRepository.Insert(withTran);
            return withTran;
        }

        public bool Withdraw(int customerId, decimal points, string reason)
        {
            var customer = _customerRepository.GetById(customerId);
            if (customer == null)
            {
                throw new ArgumentNullException("Cannot find specified customer");
            }
            if (points > customer.Points)
            {
                throw new ArgumentNullException("Cannot process this transaction. The points entered exceeds allowed value.");
            }
            var withTran = new WithdrawTransaction();
            withTran.Customer = customer;
            withTran.Points = points;
            withTran.CreatedOnUtc = DateTime.Now;
            withTran.Reason = reason;
            _withdrawTranRepository.Insert(withTran);

            customer.Points = customer.Points - points;
            _customerRepository.Update(customer);
            return true;
        }
    }
}
