﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Tmc.Admin.Models.Customers;
using Tmc.Admin.Models.Transactions;
using Tmc.BLL.Contract.ImportExport;
using Tmc.BLL.Contract.Transactions;
using Tmc.Web.Framework.Common;
using Tmc.Web.Framework.FilterAttributes;
using Tmc.Web.Framework.KendoUi;

namespace Tmc.Admin.Controllers
{
    public class TransactionController : BaseAdminController
    {
        private readonly ITransactionBiz _transactionBiz;
        private readonly IExportService _exportService;
        public TransactionController(ITransactionBiz transactionBiz, IExportService exportService)
        {
            this._transactionBiz = transactionBiz;
            this._exportService = exportService;
        }
        //
        // GET: /Transaction/
        public ActionResult List(int? customerId)
        {
            var model = new DepositTransactionListModel();
            if(customerId == null || customerId.Value <= 0)
            {
                customerId = null;
            }
            model.customerId = customerId;
            return View(model);
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, DepositTransactionListModel model)
        {
            var customers = _transactionBiz.GetAllDepositTransactions(model.customerId, model.UserName, model.DateFrom, model.DateTo, command.Page, command.PageSize);
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

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] DepositTransactionModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    var customer = model.ToEntity();

            //    var insertedCustomer = _customerBiz.InsertCustomer(customer);

            //    return Json(insertedCustomer);
            //}
            return new NullJsonResult();
        }

        public ActionResult Deposit()
        {
            return null;
        }

        [HttpPost]
        public ActionResult CustomerDeposit(int customerId, decimal amount)
        {
            bool bOk = false;
            try
            {
                bOk = _transactionBiz.Deposit(customerId, amount);
                return Json(new TmcAjaxResponse() { Success = bOk });
            }
            catch(Exception ex)
            {
                return Json(new TmcAjaxResponse(){Success = bOk, Errors = new List<string>(){ex.ToString()}});
            }
            
        }

        [AdminAuthorize]
        public ActionResult ExportExcelAll(DepositTransactionListModel model)
        {
            try
            {
                var transactions = _transactionBiz.GetAllDepositTransactions(null,null, null, null);

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

        [AdminAuthorize]
        public ActionResult ExportExcel(DepositTransactionListModel model)
        {
            try
            {
                var transactions = _transactionBiz.GetAllDepositTransactions(model.customerId,model.UserName, model.DateFrom,model.DateTo);

                byte[] bytes = null;
                using (var stream = new MemoryStream())
                {
                    _exportService.ExportDepositTransactionsToXlsx(stream, transactions);
                    bytes = stream.ToArray();
                }
                Session["Transaction_Export_Data"] = bytes;
                return new JsonResult() { Data = "Transaction_Export_Data" };
            }
            catch (Exception exc)
            {
                return new NullJsonResult();
            }
        }


        public ActionResult WithdrawList(int? customerId)
        {
            var model = new WithdrawTransactionListModel();
            if (customerId == null || customerId.Value <= 0)
            {
                customerId = null;
            }
            model.customerId = customerId;
            return View(model);
        }

        [HttpPost]
        public ActionResult WithdrawList(DataSourceRequest command, WithdrawTransactionListModel model)
        {
            var customers = _transactionBiz.GetAllWithdrawTransactions(model.customerId, model.UserName, model.DateFrom, model.DateTo, command.Page, command.PageSize);
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

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] WithdrawTransactionModel model)
        {
            return new NullJsonResult();
        }

        public ActionResult Withdraw()
        {
            return null;
        }

        [HttpPost]
        public ActionResult CustomerWithdraw(int customerId, decimal points, string reason)
        {
            bool bOk = false;
            try
            {
                bOk = _transactionBiz.Withdraw(customerId, points, reason);
                return Json(new TmcAjaxResponse() { Success = bOk });
            }
            catch (Exception ex)
            {
                return Json(new TmcAjaxResponse() { Success = bOk, Errors = new List<string>() { ex.ToString() } });
            }

        }

        [AdminAuthorize]
        public ActionResult ExportWithdrawToExcelAll(WithdrawTransactionListModel model)
        {
            try
            {
                var transactions = _transactionBiz.GetAllWithdrawTransactions(null, null, null, null);

                byte[] bytes = null;
                using (var stream = new MemoryStream())
                {
                    _exportService.ExportWithdrawTransactionsToXlsx(stream, transactions);
                    bytes = stream.ToArray();
                }
                return File(bytes, "text/xls", "withdraw_transactions.xlsx");
            }
            catch (Exception exc)
            {
                return RedirectToAction("WithdrawList");
            }
        }

        [AdminAuthorize]
        public ActionResult ExportWithdrawToExcel(WithdrawTransactionListModel model)
        {
            try
            {
                var transactions = _transactionBiz.GetAllWithdrawTransactions(model.customerId, model.UserName, model.DateFrom, model.DateTo);

                byte[] bytes = null;
                using (var stream = new MemoryStream())
                {
                    _exportService.ExportWithdrawTransactionsToXlsx(stream, transactions);
                    bytes = stream.ToArray();
                }
                Session["WithdrawTransaction_Export_Data"] = bytes;
                return new JsonResult() { Data = "WithdrawTransaction_Export_Data" };
            }
            catch (Exception exc)
            {
                return new NullJsonResult();
            }
        }
	}
}