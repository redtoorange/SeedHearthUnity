using System;
using SeedHearth.Cards;
using UnityEngine;

namespace SeedHearth.UI.CardStore
{
    public class CardStoreSlot : MonoBehaviour
    {
        public Action<Card> OnCardPuchased;

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

        public void UpdateAvailable(int playerGold)
        {
            if (playerGold >= cardInstance.GetPurchasePrice())
            {
                cardStoreSlotButton.interactable = true;
                cardInstance.SetCardDimmed(false);
            }
            else
            {
                cardStoreSlotButton.interactable = false;
                cardInstance.SetCardDimmed(true);
            }
        }

        public void OnBuyPressed()
        {
            OnCardPuchased?.Invoke(cardInstance);
        }
    }
}