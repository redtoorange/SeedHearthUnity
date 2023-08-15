using SeedHearth.Cards.Data;

namespace SeedHearth.Cards
{
    public class CardSellingController : CardController
    {
        private bool isSellable;
        private int sellPrice;

        public override void Initialize(Card pCard, CardData pCardData)
        {
            base.Initialize(pCard, pCardData);

            isSellable = cardData.isSellable;
            sellPrice = cardData.baseSellValue;
        }

        public void SellCard()
        {
        }
    }
}