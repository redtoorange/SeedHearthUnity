using SeedHearth.Managers;

namespace SeedHearth.Cards.Areas
{
    public class CardSellArea : CardArea
    {
        private ResourceManager resourceManager;

        private void Start()
        {
            resourceManager = FindFirstObjectByType<ResourceManager>();
        }

        public override void AddCard(Card cardToAdd)
        {
            base.AddCard(cardToAdd);
            cardToAdd.Hide();

            if (cardToAdd.TryGetComponent(out CardSellingController sellingController))
            {
                if (sellingController.IsSellable)
                {
                    resourceManager.AddGold(sellingController.SellPrice);
                }
            }
        }
    }
}