using SeedHearth.Cards;
using SeedHearth.Cards.Controllers;

namespace SeedHearth.GameAreas
{
    public class CardSellArea : CardArea
    {
        public override bool IsValidDropSpot(Card currentCard)
        {
            if (currentCard.TryGetComponent(out CardSellingController cardSellingController))
            {
                return cardSellingController.IsSellable && !currentCard.IsEphemeral;
            }

            return false;
        }
    }
}