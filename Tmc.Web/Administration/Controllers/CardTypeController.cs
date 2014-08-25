using System.Collections.Generic;
using System.Web.Mvc;
using Tmc.Admin.Models.CardTypes;
using Tmc.BLL.Contract.Cards;
using Tmc.Core.Domain.Cards;
using Tmc.Web.Framework.KendoUi;
using Tmc.Admin.Extensions;
using System.Linq;
using Tmc.Web.Framework.Common;

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
            return View();
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, CardTypeListModel model)
        {
            var cardTypes = _cardTypeBiz.GetAllCardTypes();
            var gridModel = new DataSourceResult
            {
                Data = cardTypes.Select(x =>
                {
                    var cardTypeModel = x.ToModel();
                    return cardTypeModel;
                }),
                Total = cardTypes.Count
            };
            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] CardTypeModel model)
        {
            if(ModelState.IsValid)
            {
                var cardType = model.ToEntity();
                var insertedCardType = _cardTypeBiz.InsertCardType(cardType);

                return Json(insertedCardType);
            }
            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult Edit(CardTypeModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new DataSourceResult() { Errors = ModelState.SerializeErrors() });
            }

            var cardType = _cardTypeBiz.GetCardTypeById(model.Id);
            if(cardType == null)
            {
                return Content("No card type could be loaded with the specified ID");
            }
            cardType = model.ToEntity(cardType);
            _cardTypeBiz.UpdateCardType(cardType);

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult Delete(CardTypeModel model)
        {
            var cardType = _cardTypeBiz.GetCardTypeById(model.Id);
            if (cardType == null)
            {
                return Content("No card type with the specified ID");
            }
            _cardTypeBiz.DeleteCardType(cardType);
            return new NullJsonResult();
        }
	}
}