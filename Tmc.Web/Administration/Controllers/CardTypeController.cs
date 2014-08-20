using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tmc.Admin.Models;
using Tmc.BLL.Contract.Cards;
using Tmc.Core.Domain.Cards;
using Tmc.Web.Framework.KendoUi;

namespace Tmc.Admin.Controllers
{
    public class CardTypeController : BaseAdminController
    {
        private readonly ICardTypeBiz _cardTypeBiz;

        public CardTypeController(ICardTypeBiz cardTypeBiz)
        {
            this._cardTypeBiz = cardTypeBiz;
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
            var cardTypes = _cardTypeBiz.GetAllCardTypes();
            return View();
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, CardTypeListModel model)
        {
            var cardTypes = _cardTypeBiz.GetAllCardTypes();
            var gridModel = new DataSourceResult
            {
                Data = new List<CardType>{new CardType()},
                //Data = cardTypes.Select(x =>
                //{
                //    var categoryModel = x.ToModel();
                //    categoryModel.Breadcrumb = x.GetFormattedBreadCrumb(_categoryService);
                //    return categoryModel;
                //}),
                Total = 0
            };
            return Json(gridModel);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string models)
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(string models)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(string models)
        {
            return View();
        }
	}
}