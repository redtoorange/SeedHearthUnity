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
            cardToAdd.CancelMovement();
            cardSellingManager.AddCard(cardToAdd);
            // base.AddCard(cardToAdd);
            // cardToAdd.Hide();

            // if (cardToAdd.TryGetComponent(out CardSellingController sellingController))
            // {
            //     if (sellingController.IsSellable)
            //     {
            //         resourceManager.AddGold(sellingController.SellPrice);
            //     }
            // }
        }
    }
}