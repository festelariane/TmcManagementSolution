using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tmc.Web.Framework.Models;
using Tmc.Web.Models.Transactions;

namespace Tmc.Web.Models.Customers
{
    public class CustomerDashboardModel : BaseModel
    {
        public CustomerDashboardModel()
        {
            CustomerInfo = new CustomerInfoModel();
            DepositTransactionListModel = new DepositTransactionListModel();
            WithdrawTransactionListModel = new WithdrawTransactionListModel();
        }
        public CustomerInfoModel CustomerInfo { get; set; }
        public DepositTransactionListModel DepositTransactionListModel { get; set; }
        public WithdrawTransactionListModel WithdrawTransactionListModel { get; set; }
    }
}