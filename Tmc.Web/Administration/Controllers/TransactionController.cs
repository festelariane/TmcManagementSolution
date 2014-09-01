using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Tmc.Admin.Models.Customers;
using Tmc.Admin.Models.Transactions;
using Tmc.BLL.Contract.Transactions;
using Tmc.Web.Framework.Common;
using Tmc.Web.Framework.KendoUi;

namespace Tmc.Admin.Controllers
{
    public class TransactionController : BaseAdminController
    {
        private readonly IDepositTransactionBiz _depositTransactionBiz;

        public TransactionController(IDepositTransactionBiz depositTransactionBiz)
        {
            this._depositTransactionBiz = depositTransactionBiz;
        }
        //
        // GET: /Transaction/
        public ActionResult List(int? customerId)
        {
            var model = new DepositTransactionListModel();
            model.customerId = customerId;
            return View(model);
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, DepositTransactionListModel model)
        {
            var customers = _depositTransactionBiz.GetAllDepositTransactions(model.customerId, model.DateFrom, model.DateTo, command.Page, command.PageSize);
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
                bOk = _depositTransactionBiz.Deposit(customerId, amount);
                return Json(new TmcAjaxResponse() { Success = bOk });
            }
            catch(Exception ex)
            {
                return Json(new TmcAjaxResponse(){Success = bOk, Errors = new List<string>(){ex.ToString()}});
            }
            
        }
	}
}