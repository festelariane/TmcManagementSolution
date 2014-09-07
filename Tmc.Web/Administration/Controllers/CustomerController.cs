using System.Collections.Generic;
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
using System;
using Tmc.Web.Framework.FilterAttributes;
using System.IO;
using Tmc.BLL.Contract.ImportExport;

namespace Tmc.Admin.Controllers
{
    [AdminAuthorize]
    public class CustomerController : BaseAdminController
    {
        private readonly ICustomerBiz _customerBiz;
        private readonly IExportService _exportService;
        public CustomerController(ICustomerBiz customerBiz, IExportService exportService)
        {
            this._customerBiz = customerBiz;
            this._exportService = exportService;
        }

        [AdminAuthorize]
        public ActionResult Index()
        {
            var model = new CardTypeListModel();
            return View(model);
        }
        [AdminAuthorize]
        public ActionResult List()
        {
            return View();
        }
        [AdminAuthorize]
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
        [AdminAuthorize]
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
        [AdminAuthorize]
        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "CreatedOnUtc, UpdatedOnUtc, LastActivityDateUtc")] CustomerModel model)
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
            customer.CardTypeId = model.CardTypeId.GetValueOrDefault(0);
            customer.FullName = model.FullName;
            customer.Points = model.Points;

            _customerBiz.UpdateCustomer(customer);

            return new NullJsonResult();
        }
        [AdminAuthorize]
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

        [AdminAuthorize]
        public ActionResult ExportExcelAll()
        {
            try
            {
                var customers = _customerBiz.GetAllCustomers(string.Empty, string.Empty);

                byte[] bytes = null;
                using (var stream = new MemoryStream())
                {
                    _exportService.ExportCustomersToXlsx(stream, customers);
                    bytes = stream.ToArray();
                }
                return File(bytes, "text/xls", "customers.xlsx");
            }
            catch (Exception exc)
            {
                return RedirectToAction("List");
            }
        }
	}
}