using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmc.Core.Domain.Cards;

namespace Tmc.BLL.Contract.Cards
{
    public interface ICardTypeBiz
    {
        IList<CardType> GetAllCardTypes();
        CardType GetCardTypeById(int cardTypeId);
        void DeleteCardType(CardType cardType);
        void UpdateCardType(CardType cardType);
        CardType InsertCardType(CardType cardType);
    }
}
