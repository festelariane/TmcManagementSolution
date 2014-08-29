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
        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, DepositTransactionListModel model)
        {
            var customers = _depositTransactionBiz.GetAllDepositTransactions(model.customerId, model.DateFrom, model.DateTo, command.Page, command.PageSize);
            var gridModel = new DataSourceResult
            {
                //CustomerId: { editable: false },
                //            Customer: {},
                //            Amount: { editable: false, type: 'number' },
                //            CreatedOnUtc: { editable: false, type: 'date', format: 'dd/MM/yyyy' },
                //            Points: { editable: false, type: 'number'},
                //            ExchangeRate: { editable: false, type: 'number' 
                Data = customers.Select(x => new {
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.UserName,
                    Amount = x.Amount,
                    CreatedOnUtc = x.CreatedOnUtc,
                    Points = x.Points,
                    ExchangeRate = x.ExchangeRate
                }),
                //Data = customers.Select(x =>
                //{
                //    var transactionModel = x.ToModel();
                //    return transactionModel;
                //}),
                Total = customers.TotalCount
            };
            return Json(gridModel);
            return null;
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