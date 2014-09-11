using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Common;
using Tmc.Core.Domain.Transaction;

namespace Tmc.BLL.Contract.Transactions
{
    public interface ITransactionBiz
    {
        #region Deposit Transaction
        IPagedList<DepositTransaction> GetAllDepositTransactions(int? customerId, string CustomerUserName, DateTime? dateFromUtc, DateTime? dateToUtc, int pageIndex = 0, int pageSize = 2147483647);
        DepositTransaction GetDepositTransactionById(int depTranId);
        void DeleteDepositTransaction(DepositTransaction depTran);
        void UpdateDepositTransaction(DepositTransaction depTran);
        DepositTransaction InsertDepositTransaction(DepositTransaction depTran);

        bool Deposit(int customerId, decimal depositAmount); 
        #endregion


        #region Withdraw Transaction
        IPagedList<WithdrawTransaction> GetAllWithdrawTransactions(int? customerId, string CustomerUserName, DateTime? dateFromUtc, DateTime? dateToUtc, int pageIndex = 0, int pageSize = 2147483647);
        WithdrawTransaction GetWithdrawTransactionById(int withTranId);
        void DeleteWithdrawTransaction(WithdrawTransaction withTran);
        void UpdateWithdrawTransaction(WithdrawTransaction withTran);
        WithdrawTransaction InsertWithdrawTransaction(WithdrawTransaction withTran);

        bool Withdraw(int customerId, decimal points, string reason);
        #endregion

    }
}
