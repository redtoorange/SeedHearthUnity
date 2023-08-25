using SeedHearth.Managers;

namespace SeedHearth.Cards.Areas
{
    public class CardSellArea : CardArea
    {
        private ResourceManager resourceManager;
        private CardSellingManager cardSellingManager;

        private void Start()
        {
            cardSellingManager = FindFirstObjectByType<CardSellingManager>();
            resourceManager = FindFirstObjectByType<ResourceManager>();
            
        }

        public override void AddCard(Card cardToAdd)
        {
            cardToAdd.SetState(CardState.BeingSold);
            cardToAdd.CancelMovement();
            cardToAdd.ResetZoom();
            cardSellingManager.AddCard(cardToAdd);
        }
    }
}