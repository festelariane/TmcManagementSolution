using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.BLL.Contract.Cards;
using Tmc.Core;
using Tmc.Core.Data;
using Tmc.Core.Domain.Cards;
using Tmc.Core.Domain.Customers;

namespace Tmc.BLL.Impl.Cards
{
    public class CardTypeBiz : ICardTypeBiz
    {
        private readonly IRepository<CardType> _cardTypeRepository;
        private readonly IRepository<Customer> _customerRepository;
        public CardTypeBiz(IRepository<CardType> cardTypeRepository, IRepository<Customer> customerRepository)
        {
            this._cardTypeRepository = cardTypeRepository;
            this._customerRepository = customerRepository;
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

            var numOfCustomer = _customerRepository.Table.Where(c => c.CardTypeId == cardType.Id).Count();
            if(numOfCustomer > 0)
            {
                throw new TmcException("Cannot delete this card type because it is being used");
            }

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
