namespace SeedHearth.Cards.Areas
{
    public class CardSellArea : CardArea
    {
        public override bool IsValidDropSpot(Card currentCard)
        {
            if (currentCard.IsEphemeral) return false;

            if (!currentCard.GetCardData().isSellable) return false;

            if (currentCard.TryGetComponent(out CardSellingController cardSellingController))
            {
                return cardSellingController.IsSellable;
            }

            return false;
        }
    }
}