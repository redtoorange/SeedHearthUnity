using SeedHearth.Cards.Data;
using UnityEngine;

namespace SeedHearth.Cards
{
    public abstract class CardController : MonoBehaviour
    {
        protected Card parentCard;
        protected CardData cardData;

        public virtual void Initialize(Card pCard, CardData pCardData)
        {
            parentCard = pCard;
            cardData = pCardData;
        }
    }
}