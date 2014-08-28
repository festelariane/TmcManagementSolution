using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tmc.Admin.Models.Customers;
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
        public ActionResult List(DataSourceRequest command, CustomerListModel model)
        {
            //var customers = _depositTransactionBiz.GetAllDepositTransactions(model.SearchUserName, model.SearchFullName, command.Page, command.PageSize);
            //var gridModel = new DataSourceResult
            //{
            //    Data = customers.Select(x =>
            //    {
            //        var customerModel = x.ToModel();
            //        return customerModel;
            //    }),
            //    Total = customers.TotalCount
            //};
            //return Json(gridModel);
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
	}
}