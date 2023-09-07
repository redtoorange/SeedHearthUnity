using SeedHearth.Cards;

namespace SeedHearth.GameAreas
{
    public class CardDiscardArea : CardArea
    {
        public override void AddCard(Card cardToAdd)
        {
            cardToAdd.SetState(CardState.InDiscardPile);
            base.AddCard(cardToAdd);
            cardToAdd.transform.SetAsLastSibling();
        }
    }
}