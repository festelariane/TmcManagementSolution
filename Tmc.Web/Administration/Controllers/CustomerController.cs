﻿using System.Collections.Generic;
using System.Web.Mvc;
using Tmc.Admin.Models.CardTypes;
using Tmc.BLL.Contract.Cards;
using Tmc.Core.Domain.Cards;
using Tmc.Web.Framework.KendoUi;
using Tmc.Admin.Extensions;
using System.Linq;
using Tmc.Web.Framework.Common;
using Tmc.BLL.Contract.Customers;
using Tmc.Admin.Models.Customers;

namespace Tmc.Admin.Controllers
{
    public class CustomerController : BaseAdminController
    {
        private readonly ICustomerBiz _customerBiz;

        public CustomerController(ICustomerBiz customerBiz)
        {
            this._customerBiz = customerBiz;
        }
        //
        // GET: /CardType/
        public ActionResult Index()
        {
            var model = new CardTypeListModel();
            return View(model);
        }
        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, CustomerListModel model)
        {
            var customers = _customerBiz.GetAllCustomers(model.SearchUserName, model.SearchFullName, command.Page, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = customers.Select(x =>
                {
                    var customerModel = x.ToModel();
                    return customerModel;
                }),
                Total = customers.TotalCount
            };
            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id, Points, CreatedOnUtc, UpdatedOnUtc, LastActivityDateUtc")] CustomerModel model)
        {
            if(ModelState.IsValid)
            {
                var customer = model.ToEntity();
                var insertedCustomer = _customerBiz.InsertCustomer(customer);

                return Json(insertedCustomer);
            }
            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult Edit(CustomerModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new DataSourceResult() { Errors = ModelState.SerializeErrors() });
            }

            var customer = _customerBiz.GetCustomerById(model.Id);
            if(customer == null)
            {
                return Content("No customer could be loaded with the specified ID");
            }
            customer = model.ToEntity(customer);
            _customerBiz.UpdateCustomer(customer);

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult Delete(CustomerModel model)
        {
            var customer = _customerBiz.GetCustomerById(model.Id);
            if (customer == null)
            {
                return Content("No customer with the specified ID");
            }
            _customerBiz.DeleteCustomer(customer);
            return new NullJsonResult();
        }
	}
}