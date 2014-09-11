using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Domain.Customers;
using Tmc.Core.Domain.Transaction;

namespace Tmc.BLL.Contract.ImportExport
{
    public interface IExportService
    {
        void ExportCustomersToXlsx(Stream stream, IList<Customer> customers);
        void ExportDepositTransactionsToXlsx(Stream stream, IList<DepositTransaction> transactions);
        void ExportWithdrawTransactionsToXlsx(Stream stream, IList<WithdrawTransaction> transactions);
        
    }
}
