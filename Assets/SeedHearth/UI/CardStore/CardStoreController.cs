using System.Collections.Generic;
using SeedHearth.Cards;
using SeedHearth.Data;
using SeedHearth.Managers;
using UnityEngine;

namespace SeedHearth.UI.CardStore
{
    public class CardStoreController : MonoBehaviour
    {
        [SerializeField] private GameObject storeUIWindow;
        [SerializeField] private RectTransform cardStoreHolder;
        [SerializeField] private DeckData storeDeck;
        [SerializeField] private CardStoreSlot storeSlotPrefab;
        [SerializeField] private CardManager cardManager;
        [SerializeField] private ResourceManager playerResouces;

        private List<CardStoreSlot> cardSlots = new List<CardStoreSlot>();

        private void Start()
        {
            cardManager = FindFirstObjectByType<CardManager>();
            playerResouces = FindFirstObjectByType<ResourceManager>();
            storeUIWindow.SetActive(false);
            InitializeStore();
        }

        private void Update()
        {
            if (storeUIWindow.activeSelf)
            {
                int currentGold = playerResouces.GetGold();
                foreach (CardStoreSlot cardSlot in cardSlots)
                {
                    cardSlot.UpdateAvailable(currentGold);
                }
            }
        }

        public void ToggleStoreWindow()
        {
            storeUIWindow.SetActive(!storeUIWindow.activeSelf);
        }

        private void InitializeStore()
        {
            foreach (DeckCardData cardData in storeDeck.deckCardData)
            {
                CardStoreSlot newSlot = Instantiate(storeSlotPrefab, cardStoreHolder);
                Card newSlotCardTemplate = Instantiate(cardData.cardPrefab);
                newSlotCardTemplate.Initialize();
                newSlot.Initialize(newSlotCardTemplate);
                newSlot.OnCardPuchased += HandleCardPurchased;
                cardSlots.Add(newSlot);
            }
        }

        private void HandleCardPurchased(Card card)
        {
            int purchasePrice = card.GetPurchasePrice();
            if (playerResouces.HasEnoughGold(purchasePrice))
            {
                playerResouces.TakeGold(purchasePrice);
                Card newCard = cardManager.SpawnNewCard(card);
                newCard.FlipToFont();
                cardManager.DiscardCardFromPlay(newCard);
            }
        }
    }
}