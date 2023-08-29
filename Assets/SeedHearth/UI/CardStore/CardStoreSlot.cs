using SeedHearth.Cards;
using UnityEngine;

namespace SeedHearth.UI.CardStore
{
    public class CardStoreSlot : MonoBehaviour
    {
        [SerializeField] private RectTransform cardSpawnSlot;
        [SerializeField] private CardStoreSlotButton cardStoreSlotButton;
        private Card cardInstance;

        public void Initialize(Card card)
        {
            cardInstance = card;
            cardInstance.transform.SetParent(cardSpawnSlot, false);
            cardInstance.FlipToFont();
            cardInstance.SetState(CardState.InStore);
            cardStoreSlotButton.Initialize(card.GetCardData());
        }

        private void Update()
        {
            Debug.Log("Updating card slot");
        }

        public void OnBuyPressed()
        {
            Debug.Log("Trying to purchase " + cardInstance.GetCardData().cardTitle);
        }
    }
}