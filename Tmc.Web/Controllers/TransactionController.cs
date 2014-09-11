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

namespace Tmc.Web.Controllers
{
    public class TransactionController : BaseFrontEndController
    {
        private readonly ITransactionBiz _depositTransactionBiz;
        private readonly IExportService _exportService;
        private readonly IWorkContext _workContext;
        public TransactionController(ITransactionBiz depositTransactionBiz, IExportService exportService, IWorkContext workContext)
        {
            this._depositTransactionBiz = depositTransactionBiz;
            this._exportService = exportService;
            this._workContext = workContext;
        }
        //
        // GET: /Transaction/
        public ActionResult List(int? customerId)
        {
            var currentCustomer = _workContext.CurrentCustomer;
            if (currentCustomer == null)
            {
                return new EmptyResult();
            }
            if (customerId == null || customerId.Value <= 0 || customerId.Value != currentCustomer.Id)
            {
                return new EmptyResult();
            }
            return  View(customerId.Value);
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, int? customerId)
        {
            var currentCustomer = _workContext.CurrentCustomer;
            if(currentCustomer == null || customerId == null || customerId.Value != currentCustomer.Id)
            {
                return new NullJsonResult();
            }
            
            var customers = _depositTransactionBiz.GetAllDepositTransactions(currentCustomer.Id, null, null, null, command.Page, command.PageSize);
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
                var transactions = _depositTransactionBiz.GetAllDepositTransactions(null, null, null, null);

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
	}
}