using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Common;
using Tmc.Core.Domain.Transaction;

namespace Tmc.BLL.Contract.Transactions
{
    public interface IDepositTransactionBiz
    {
        IPagedList<DepositTransaction> GetAllDepositTransactions(int? customerId, string CustomerUserName, DateTime? dateFromUtc, DateTime? dateToUtc , int pageIndex = 0, int pageSize = 2147483647);
        DepositTransaction GetDepositTransactionById(int depTranId);
        void DeleteDepositTransaction(DepositTransaction depTran);
        void UpdateDepositTransaction(DepositTransaction depTran);
        DepositTransaction InsertDepositTransaction(DepositTransaction depTran);

        bool Deposit(int customerId, decimal depositAmount);
    }
}
