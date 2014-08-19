using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tmc.Admin.Models;
using Tmc.BLL.Contract.Cards;

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
	}
}