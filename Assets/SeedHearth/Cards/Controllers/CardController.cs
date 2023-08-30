using SeedHearth.Data;
using UnityEngine;

namespace SeedHearth.Cards.Controllers
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