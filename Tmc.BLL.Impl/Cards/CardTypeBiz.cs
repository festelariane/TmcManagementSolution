using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.BLL.Contract.Cards;
using Tmc.Core.Data;
using Tmc.Core.Domain.Cards;

namespace Tmc.BLL.Impl.Cards
{
    public class CardTypeBiz : ICardTypeBiz
    {
        private readonly IRepository<CardType> _cardTypeRepository;

        public CardTypeBiz(IRepository<CardType> cardTypeRepository)
        {
            this._cardTypeRepository = cardTypeRepository;
        }
        public IList<CardType> GetAllCardTypes()
        {
            var allCardTypes = _cardTypeRepository.Table.OrderBy(ct => ct.DisplayOrder).OrderBy(ct => ct.Name).ToList();

            return allCardTypes;
        }

        public CardType GetCardTypeById(int cardTypeId)
        {
            if(cardTypeId <= 0)
            {
                return null;
            }
            return _cardTypeRepository.GetById(cardTypeId);
        }

        public void DeleteCardType(CardType cardType)
        {
            if (cardType == null)
                throw new ArgumentNullException("cardType");

            _cardTypeRepository.Delete(cardType);
        }

        public void UpdateCardType(CardType cardType)
        {
            if (cardType == null)
                throw new ArgumentNullException("cardType");

            _cardTypeRepository.Update(cardType);
        }

        public CardType InsertCardType(CardType cardType)
        {
            if (cardType == null)
                throw new ArgumentNullException("cardType");

            _cardTypeRepository.Insert(cardType);
            return cardType;
        }
    }
}
