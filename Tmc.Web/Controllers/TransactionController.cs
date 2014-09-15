using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Tmc.BLL.Contract.ImportExport;
using Tmc.BLL.Contract.Transactions;
using Tmc.Core.Common;
using Tmc.Core.Domain.Customers;
using Tmc.Web.Framework.Common;
using Tmc.Web.Framework.FilterAttributes;
using Tmc.Web.Framework.KendoUi;
using Tmc.Web.Models.Transactions;

namespace Tmc.Web.Controllers
{
    [AdminAuthorize("RegisteredUsers")]
    public class TransactionController : BaseFrontEndController
    {
        private readonly ITransactionBiz _transactionBiz;
        private readonly IExportService _exportService;
        private readonly IWorkContext _workContext;
        public TransactionController(ITransactionBiz transactionBiz, IExportService exportService, IWorkContext workContext)
        {
            this._transactionBiz = transactionBiz;
            this._exportService = exportService;
            this._workContext = workContext;
        }
        //
        // GET: /Transaction/
        public ActionResult List()
        {
            var currentCustomer = _workContext.CurrentCustomer;
            if (currentCustomer == null || currentCustomer.Id <= 0)
            {
                return new EmptyResult();
            }
            var model = new DepositTransactionListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, DepositTransactionListModel model)
        {
            var currentCustomer = _workContext.CurrentCustomer;
            if (currentCustomer == null || currentCustomer.Id <= 0)
            {
                return new NullJsonResult();
            }
            
            var customers = _transactionBiz.GetAllDepositTransactions(currentCustomer.Id, null, model.DateFrom, model.DateTo, command.Page, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = customers.Select(x => new {
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.UserName,
                    Amount = x.Amount,
                    CreatedOnUtc = x.CreatedOnUtc,
                    Points = x.Points,
                    ExchangeRate = x.ExchangeRate
                }),
                Total = customers.TotalCount
            };
            return Json(gridModel);
        }

        [AdminAuthorize]
        public ActionResult ExportExcelAll()
        {
            try
            {
                var transactions = _transactionBiz.GetAllDepositTransactions(null, null, null, null);

                byte[] bytes = null;
                using (var stream = new MemoryStream())
                {
                    _exportService.ExportDepositTransactionsToXlsx(stream, transactions);
                    bytes = stream.ToArray();
                }
                return File(bytes, "text/xls", "transactions.xlsx");
            }
            catch (Exception exc)
            {
                return RedirectToAction("List");
            }
        }

        public ActionResult WithdrawList()
        {
            var currentCustomer = _workContext.CurrentCustomer;
            if (currentCustomer == null || currentCustomer.Id <= 0)
            {
                return new EmptyResult();
            }
            var model = new WithdrawTransactionListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult WithdrawList(DataSourceRequest command, WithdrawTransactionListModel model)
        {
            var currentCustomer = _workContext.CurrentCustomer;
            if (currentCustomer == null || currentCustomer.Id <= 0)
            {
                return new EmptyResult();
            }
            var customers = _transactionBiz.GetAllWithdrawTransactions(currentCustomer.Id, null, model.DateFrom, model.DateTo, command.Page, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = customers.Select(x => new
                {
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.UserName,
                    CreatedOnUtc = x.CreatedOnUtc,
                    Points = x.Points,
                    Reason = x.Reason
                }),
                Total = customers.TotalCount
            };
            return Json(gridModel);
        }

	}
}